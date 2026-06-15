using BackendAPI.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Services;

public class RefreshTokenCleanupService : BackgroundService
{
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly ILogger<RefreshTokenCleanupService> logger;
    private readonly TimeSpan cleanupInterval;
    private readonly TimeSpan revokedTokenRetention;

    public RefreshTokenCleanupService(
        IServiceScopeFactory serviceScopeFactory,
        IConfiguration configuration,
        ILogger<RefreshTokenCleanupService> logger)
    {
        this.serviceScopeFactory = serviceScopeFactory;
        this.logger = logger;
        var cleanupIntervalMinutes = configuration.GetValue("RefreshTokens:CleanupIntervalMinutes", 60);
        cleanupInterval = TimeSpan.FromMinutes(Math.Max(1, cleanupIntervalMinutes));
        var revokedTokenRetentionDays = configuration.GetValue("RefreshTokens:RevokedTokenRetentionDays", 7);
        revokedTokenRetention = TimeSpan.FromDays(Math.Max(0, revokedTokenRetentionDays));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(cleanupInterval);

        do
        {
            await DeleteExpiredRefreshTokens(stoppingToken);
        }
        while (await timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task DeleteExpiredRefreshTokens(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            var now = DateTime.UtcNow;
            var revokedBeforeUtc = now.Subtract(revokedTokenRetention);
            var deletedCount = await dbContext.RefreshTokens
                .Where(refreshToken =>
                    refreshToken.ExpiresAtUtc <= now ||
                    refreshToken.RevokedAtUtc <= revokedBeforeUtc)
                .ExecuteDeleteAsync(cancellationToken);

            if (deletedCount > 0)
            {
                logger.LogInformation("Deleted {DeletedCount} expired or revoked refresh tokens.", deletedCount);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to delete expired refresh tokens.");
        }
    }
}

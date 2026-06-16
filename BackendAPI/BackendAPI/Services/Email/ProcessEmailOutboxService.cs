using BackendAPI.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BackendAPI.Services.Email;

public sealed class ProcessEmailOutboxService : BackgroundService
{
    private readonly EmailOutboxOptions outboxOptions;
    private readonly SmtpEmailOptions smtpOptions;
    private readonly IServiceScopeFactory scopeFactory;
    private readonly ILogger<ProcessEmailOutboxService> logger;

    public ProcessEmailOutboxService(
        IOptions<EmailOutboxOptions> outboxOptions,
        IOptions<SmtpEmailOptions> smtpOptions,
        IServiceScopeFactory scopeFactory,
        ILogger<ProcessEmailOutboxService> logger)
    {
        this.outboxOptions = outboxOptions.Value;
        this.smtpOptions = smtpOptions.Value;
        this.scopeFactory = scopeFactory;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!outboxOptions.Enabled)
        {
            logger.LogInformation("Email outbox processing is disabled.");
            return;
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(smtpOptions.Host))
                {
                    logger.LogInformation("SMTP is not configured. Email outbox messages will remain pending.");
                }
                else
                {
                    await ProcessBatchAsync(stoppingToken);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                return;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Email outbox processing failed.");
            }

            await Task.Delay(TimeSpan.FromSeconds(outboxOptions.PollIntervalSeconds), stoppingToken);
        }
    }

    private async Task ProcessBatchAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
        var now = DateTime.UtcNow;

        var messages = await context.EmailOutboxMessages
            .Where(message => message.SentOnUtc == null
                && message.RetryCount < outboxOptions.MaxRetryCount
                && (message.NextAttemptOnUtc == null || message.NextAttemptOnUtc <= now))
            .OrderBy(message => message.CreatedOnUtc)
            .Take(outboxOptions.BatchSize)
            .ToListAsync(cancellationToken);

        foreach (var message in messages)
        {
            try
            {
                await emailSender.SendEmailAsync(message.ToEmail, message.Subject, message.Body, cancellationToken);
                message.SentOnUtc = DateTime.UtcNow;
                message.Error = null;
            }
            catch (Exception ex)
            {
                message.RetryCount++;
                message.LastAttemptOnUtc = DateTime.UtcNow;
                message.NextAttemptOnUtc = DateTime.UtcNow.Add(GetRetryDelay(message.RetryCount));
                message.Error = ex.Message;
                logger.LogWarning(ex, "Failed to send email outbox message {MessageId}.", message.Id);
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }

    private static TimeSpan GetRetryDelay(int retryCount)
    {
        var seconds = Math.Min(Math.Pow(2, retryCount), 300);
        return TimeSpan.FromSeconds(seconds);
    }
}

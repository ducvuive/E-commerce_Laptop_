using System.Text.Json;
using BackendAPI.Application.UseCases.Orders.Events;
using BackendAPI.Persistence.Data;
using BackendAPI.Services.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BackendAPI.Services.Messaging;

public sealed class ProcessOutboxMessagesService : BackgroundService
{
    private readonly OutboxOptions outboxOptions;
    private readonly RabbitMqOptions rabbitMqOptions;
    private readonly IServiceScopeFactory scopeFactory;
    private readonly ILogger<ProcessOutboxMessagesService> logger;

    public ProcessOutboxMessagesService(
        IOptions<OutboxOptions> outboxOptions,
        IOptions<RabbitMqOptions> rabbitMqOptions,
        IServiceScopeFactory scopeFactory,
        ILogger<ProcessOutboxMessagesService> logger)
    {
        this.outboxOptions = outboxOptions.Value;
        this.rabbitMqOptions = rabbitMqOptions.Value;
        this.scopeFactory = scopeFactory;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!outboxOptions.Enabled)
        {
            logger.LogInformation("Outbox processing is disabled.");
            return;
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (!rabbitMqOptions.Enabled)
                {
                    logger.LogInformation("RabbitMQ is disabled. Outbox messages will remain pending.");
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
                logger.LogWarning(ex, "Outbox processing failed.");
            }

            await Task.Delay(TimeSpan.FromSeconds(outboxOptions.PollIntervalSeconds), stoppingToken);
        }
    }

    private async Task ProcessBatchAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        var publisher = scope.ServiceProvider.GetRequiredService<IOrderEventPublisher>();
        var now = DateTime.UtcNow;

        var messages = await context.OutboxMessages
            .Where(message => message.ProcessedOnUtc == null
                && message.RetryCount < outboxOptions.MaxRetryCount
                && (message.NextAttemptOnUtc == null || message.NextAttemptOnUtc <= now))
            .OrderBy(message => message.OccurredOnUtc)
            .Take(outboxOptions.BatchSize)
            .ToListAsync(cancellationToken);

        foreach (var message in messages)
        {
            try
            {
                if (message.Type != nameof(OrderPlacedEvent))
                {
                    throw new InvalidOperationException($"Unsupported outbox message type '{message.Type}'.");
                }

                var orderPlaced = JsonSerializer.Deserialize<OrderPlacedEvent>(message.Content)
                    ?? throw new InvalidOperationException("Outbox message content is invalid.");

                await publisher.PublishOrderPlacedAsync(orderPlaced, cancellationToken);
                message.ProcessedOnUtc = DateTime.UtcNow;
                message.Error = null;
            }
            catch (Exception ex)
            {
                message.RetryCount++;
                message.LastAttemptOnUtc = DateTime.UtcNow;
                message.NextAttemptOnUtc = DateTime.UtcNow.Add(GetRetryDelay(message.RetryCount));
                message.Error = ex.Message;
                logger.LogWarning(ex, "Failed to publish outbox message {MessageId}.", message.Id);
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

using System.Text;
using System.Text.Json;
using BackendAPI.Persistence.Data;
using BackendAPI.Persistence.Messaging;
using BackendAPI.Services.Email;
using BackendAPI.Services.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShareView.Constants;

namespace BackendAPI.Services.Messaging;

public sealed class OrderPlacedConsumerService : BackgroundService
{
    private readonly RabbitMqOptions options;
    private readonly IServiceScopeFactory scopeFactory;
    private readonly ILogger<OrderPlacedConsumerService> logger;

    public OrderPlacedConsumerService(
        IOptions<RabbitMqOptions> options,
        IServiceScopeFactory scopeFactory,
        ILogger<OrderPlacedConsumerService> logger)
    {
        this.options = options.Value;
        this.scopeFactory = scopeFactory;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!options.Enabled)
        {
            logger.LogInformation("RabbitMQ consumer is disabled.");
            return;
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ConsumeAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                return;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "RabbitMQ consumer disconnected. Retrying in 15 seconds.");
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
        }
    }

    private async Task ConsumeAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = options.HostName,
            Port = options.Port,
            UserName = options.UserName,
            Password = options.Password,
            VirtualHost = options.VirtualHost
        };

        await using var connection = await factory.CreateConnectionAsync(stoppingToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await DeclareTopologyAsync(channel, stoppingToken);
        await channel.BasicQosAsync(0, 1, false, stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (_, eventArgs) =>
        {
            try
            {
                await HandleMessageAsync(
                    eventArgs.Body,
                    eventArgs.BasicProperties.MessageId,
                    stoppingToken);
                await channel.BasicAckAsync(eventArgs.DeliveryTag, false, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process order message. Sending message to dead-letter queue.");
                await channel.BasicNackAsync(eventArgs.DeliveryTag, false, false, stoppingToken);
            }
        };

        await channel.BasicConsumeAsync(options.OrderPlacedQueue, false, consumer, stoppingToken);
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task DeclareTopologyAsync(IChannel channel, CancellationToken cancellationToken)
    {
        await channel.ExchangeDeclareAsync(
            exchange: options.DeadLetterExchange,
            type: ExchangeType.Fanout,
            durable: true,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            queue: options.DeadLetterQueue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        await channel.QueueBindAsync(
            queue: options.DeadLetterQueue,
            exchange: options.DeadLetterExchange,
            routingKey: string.Empty,
            arguments: null,
            cancellationToken: cancellationToken);

        var queueArguments = new Dictionary<string, object?>
        {
            ["x-dead-letter-exchange"] = options.DeadLetterExchange
        };

        await channel.QueueDeclareAsync(
            queue: options.OrderPlacedQueue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: queueArguments,
            cancellationToken: cancellationToken);
    }

    private async Task HandleMessageAsync(
        ReadOnlyMemory<byte> body,
        string? brokerMessageId,
        CancellationToken cancellationToken)
    {
        var json = Encoding.UTF8.GetString(body.Span);
        var orderPlaced = JsonSerializer.Deserialize<OrderPlacedEvent>(json);
        if (orderPlaced == null)
        {
            throw new InvalidOperationException("Order message is invalid.");
        }

        var messageId = string.IsNullOrWhiteSpace(brokerMessageId)
            ? orderPlaced.CorrelationId
            : brokerMessageId;

        if (string.IsNullOrWhiteSpace(messageId))
        {
            throw new InvalidOperationException("Order message has no message id.");
        }

        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var alreadyProcessed = await context.ProcessedMessages.AnyAsync(
            message => message.MessageId == messageId &&
                message.ConsumerName == nameof(OrderPlacedConsumerService),
            cancellationToken);

        if (alreadyProcessed)
        {
            logger.LogInformation("Message {MessageId} was already processed.", messageId);
            return;
        }

        var invoice = await context.Invoice
            .Include(x => x.InvoiceDetail)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.InvoiceId == orderPlaced.InvoiceId, cancellationToken);

        if (invoice == null)
        {
            logger.LogWarning("Order {InvoiceId} no longer exists. Message will be acknowledged.", orderPlaced.InvoiceId);
            context.ProcessedMessages.Add(CreateProcessedMessage(messageId));
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return;
        }

        if (invoice.Status != InvoiceStatus.Pending)
        {
            logger.LogInformation(
                "Order {InvoiceId} has status {Status}. Message will be acknowledged.",
                invoice.InvoiceId,
                invoice.Status);
            context.ProcessedMessages.Add(CreateProcessedMessage(messageId));
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return;
        }

        var confirmationEmail = new OrderConfirmationEmail
        {
            InvoiceId = invoice.InvoiceId,
            ToEmail = orderPlaced.CustomerEmail,
            Receiver = invoice.Receiver ?? orderPlaced.Receiver,
            Address = invoice.Address ?? orderPlaced.Address,
            Phone = invoice.Phone ?? orderPlaced.Phone,
            Total = invoice.Total.GetValueOrDefault(),
            Items = invoice.InvoiceDetail.Select(detail => new OrderConfirmationEmailItem
            {
                ProductName = detail.Product?.NameProduct ?? $"Product #{detail.ProductId}",
                Quantity = detail.Quantity.GetValueOrDefault(),
                Price = detail.Product?.Price ?? 0
            }).ToList()
        };

        if (!string.IsNullOrWhiteSpace(confirmationEmail.ToEmail))
        {
            context.EmailOutboxMessages.Add(new EmailOutboxMessage
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoice.InvoiceId,
                ToEmail = confirmationEmail.ToEmail,
                Subject = OrderConfirmationEmailBuilder.BuildSubject(confirmationEmail),
                Body = OrderConfirmationEmailBuilder.BuildBody(confirmationEmail),
                CreatedOnUtc = DateTime.UtcNow
            });
        }

        invoice.Status = InvoiceStatus.Confirmed;
        context.ProcessedMessages.Add(CreateProcessedMessage(messageId));
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }

    private static ProcessedMessage CreateProcessedMessage(string messageId)
    {
        return new ProcessedMessage
        {
            MessageId = messageId,
            ConsumerName = nameof(OrderPlacedConsumerService),
            ProcessedOnUtc = DateTime.UtcNow
        };
    }
}

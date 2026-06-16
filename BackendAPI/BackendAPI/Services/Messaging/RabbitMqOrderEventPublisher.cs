using System.Text;
using System.Text.Json;
using BackendAPI.Services.Orders;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace BackendAPI.Services.Messaging;

public sealed class RabbitMqOrderEventPublisher : IOrderEventPublisher
{
    private readonly RabbitMqOptions options;
    private readonly ILogger<RabbitMqOrderEventPublisher> logger;

    public RabbitMqOrderEventPublisher(
        IOptions<RabbitMqOptions> options,
        ILogger<RabbitMqOrderEventPublisher> logger)
    {
        this.options = options.Value;
        this.logger = logger;
    }

    public async Task PublishOrderPlacedAsync(OrderPlacedEvent orderPlaced, CancellationToken cancellationToken)
    {
        if (!options.Enabled)
        {
            throw new InvalidOperationException("RabbitMQ publishing is disabled.");
        }

        var factory = new ConnectionFactory
        {
            HostName = options.HostName,
            Port = options.Port,
            UserName = options.UserName,
            Password = options.Password,
            VirtualHost = options.VirtualHost
        };

        await using var connection = await factory.CreateConnectionAsync(cancellationToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await DeclareTopologyAsync(channel, cancellationToken);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(orderPlaced));
        var properties = new BasicProperties
        {
            ContentType = "application/json",
            CorrelationId = orderPlaced.CorrelationId,
            MessageId = orderPlaced.CorrelationId,
            Persistent = true,
            Type = nameof(OrderPlacedEvent)
        };

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: options.OrderPlacedQueue,
            mandatory: false,
            basicProperties: properties,
            body: body,
            cancellationToken: cancellationToken);
    }

    public async Task DeclareTopologyAsync(IChannel channel, CancellationToken cancellationToken)
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
}

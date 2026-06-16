namespace BackendAPI.Services.Messaging;

public sealed class RabbitMqOptions
{
    public bool Enabled { get; set; } = true;
    public string HostName { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string UserName { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string VirtualHost { get; set; } = "/";
    public string OrderPlacedQueue { get; set; } = "orders.placed";
    public string DeadLetterExchange { get; set; } = "orders.dead-letter";
    public string DeadLetterQueue { get; set; } = "orders.placed.dead-letter";
}

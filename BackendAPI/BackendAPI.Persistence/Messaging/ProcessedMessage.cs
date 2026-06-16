namespace BackendAPI.Persistence.Messaging;

public sealed class ProcessedMessage
{
    public string MessageId { get; set; } = string.Empty;
    public string ConsumerName { get; set; } = string.Empty;
    public DateTime ProcessedOnUtc { get; set; }
}

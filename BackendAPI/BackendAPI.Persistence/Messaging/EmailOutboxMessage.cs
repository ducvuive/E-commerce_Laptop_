namespace BackendAPI.Persistence.Messaging;

public sealed class EmailOutboxMessage
{
    public Guid Id { get; set; }
    public int? InvoiceId { get; set; }
    public string ToEmail { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? SentOnUtc { get; set; }
    public DateTime? LastAttemptOnUtc { get; set; }
    public DateTime? NextAttemptOnUtc { get; set; }
    public int RetryCount { get; set; }
    public string? Error { get; set; }
}

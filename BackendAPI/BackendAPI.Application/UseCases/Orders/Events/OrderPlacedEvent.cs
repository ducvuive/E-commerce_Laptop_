namespace BackendAPI.Application.UseCases.Orders.Events;

public sealed class OrderPlacedEvent
{
    public int InvoiceId { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string Receiver { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public long Total { get; set; }
    public string PaymentMethod { get; set; } = "COD";
    public string CorrelationId { get; set; } = string.Empty;
}

namespace BackendAPI.Services.Email;

public sealed class OrderConfirmationEmail
{
    public int InvoiceId { get; set; }
    public string ToEmail { get; set; } = string.Empty;
    public string Receiver { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public long Total { get; set; }
    public List<OrderConfirmationEmailItem> Items { get; set; } = new();
}

public sealed class OrderConfirmationEmailItem
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public long Price { get; set; }
}

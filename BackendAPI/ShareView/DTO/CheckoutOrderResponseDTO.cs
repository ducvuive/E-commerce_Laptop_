namespace ShareView.DTO;

public class CheckoutOrderResponseDTO
{
    public int InvoiceId { get; set; }
    public int Status { get; set; }
    public long Total { get; set; }
    public string Message { get; set; } = string.Empty;
}

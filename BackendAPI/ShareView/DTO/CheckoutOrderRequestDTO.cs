using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO;

public class CheckoutOrderRequestDTO
{
    [Required]
    [MaxLength(100)]
    public string? Receiver { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Address { get; set; }

    [Required]
    [Phone]
    public string? Phone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [MaxLength(30)]
    public string PaymentMethod { get; set; } = "COD";

    [MinLength(1)]
    public List<CheckoutOrderItemDTO> Items { get; set; } = new();
}

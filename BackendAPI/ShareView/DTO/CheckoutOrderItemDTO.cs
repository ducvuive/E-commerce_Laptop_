using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO;

public class CheckoutOrderItemDTO
{
    [Range(1, int.MaxValue)]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class InvoiceDetailDTO
    {
        [Key]
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}

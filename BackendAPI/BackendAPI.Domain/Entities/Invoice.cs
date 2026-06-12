using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackendAPI.Domain.Entities
{
    public partial class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        [Required]
        public DateTime? DateReceived { get; set; }

        [MaxLength(100)]
        public string? Receiver { get; set; }
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [Required]
        public long? Total { get; set; }
        public int? Status { get; set; }
        public string? CustomerId { get; set; }
        public virtual List<InvoiceDetail> InvoiceDetail { get; set; } = new List<InvoiceDetail>();
    }
}

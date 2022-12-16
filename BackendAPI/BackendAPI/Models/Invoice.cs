using BackendAPI.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackendAPI.Models
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
        [ForeignKey("CustomerId")]
        public UserIdentity? Customer { get; set; }
        public virtual List<InvoiceDetail> InvoiceDetail { get; set; } = new List<InvoiceDetail>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int ScreenId { get; set; }

        public int ProcessorId { get; set; }
        public int RamId { get; set; }
        //public int CongKetNoiId { get; set; }
        public int CategoryId { get; set; }
        public string? NameProduct { get; set; }
        public int? Quantity { get; set; }
        public long? Price { get; set; }
        public string? Image { get; set; }
        public float? Rating { get; set; }
        public bool IsDisable { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [ForeignKey("ScreenId")]
        public virtual Screen Screen { get; set; }
        [ForeignKey("ProcessorId")]
        public virtual Processor Processor { get; set; }
        [ForeignKey("RamId")]
        public virtual Ram Ram { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual List<InvoiceDetail> CTHD { get; set; } = new List<InvoiceDetail>();
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}

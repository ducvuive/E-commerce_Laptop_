using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models
{
    public partial class Ram
    {
        [Key]
        public int RamId { get; set; }
        [MaxLength(100)]
        public string? Capacity { get; set; }
        [MaxLength(100)]
        public string? Typee { get; set; }
        [MaxLength(100)]
        public string? BusRam { get; set; }
        [MaxLength(100)]
        public string? MaxSupport { get; set; }

        public virtual List<Product> Product { get; set; } = new List<Product>();
    }
}

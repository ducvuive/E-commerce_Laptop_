using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackendAPI.Models
{
    public partial class Processor
    {
        [Key]
        public int ProcessorId { get; set; }

        [MaxLength(100)]
        public string? CPUTechnology { get; set; }

        [MaxLength(100)]
        public int? Multiplier { get; set; } = default;

        [MaxLength(100)]
        public int? Thread { get; set; } = default;
        public string? Speed { get; set; }
        public string? MaxSpeed { get; set; }

        [MaxLength(100)]
        public string? Cache { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}

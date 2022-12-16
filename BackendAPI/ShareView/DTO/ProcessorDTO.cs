using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ShareView.DTO
{
    public partial class ProcessorDTO
    {
        [Key]
        public int ProcessorId { get; set; }
        public string? CPUTechnology { get; set; }
        public int? Multiplier { get; set; } = default;
        public int? Thread { get; set; } = default;
        public string? Speed { get; set; }
        public string? MaxSpeed { get; set; }
        public string? Cache { get; set; }

        //public virtual List<SanPhamDTO> SanPham { get; set; } = new List<SanPhamDTO>();
    }
}

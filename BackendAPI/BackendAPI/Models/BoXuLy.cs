using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackendAPI.Models
{
    public partial class BoXuLy
    {
        [Key]
        public int BoXuLyId { get; set; }

        [MaxLength(100)]
        public string? CongNgheCPU { get; set; }

        [MaxLength(100)]
        public int? SoNhan { get; set; } = default;

        [MaxLength(100)]
        public int? SoLuong { get; set; } = default;
        public string? TocDoCPU { get; set; }
        public string? ToCDoToiDa { get; set; }

        [MaxLength(100)]
        public string? BoNhoDem { get; set; }

        public virtual List<SanPham> SanPham { get; set; } = new List<SanPham>();
    }
}

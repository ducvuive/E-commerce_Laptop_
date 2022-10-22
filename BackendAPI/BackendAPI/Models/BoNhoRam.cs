using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackendAPI.Models
{
    public partial class BoNhoRam
    {
        [Key]
        public int RamId { get; set; }
        [MaxLength(100)]
        public string? DungLuongRam { get; set; }
        [MaxLength(100)]
        public string? LoaiRam { get; set; }
        [MaxLength(100)]
        public string? BusRam { get; set; }
        [MaxLength(100)]
        public string? HoTroToiDa { get; set; }

        public virtual List<SanPham> SanPham { get; set; } = new List<SanPham>();
    }
}

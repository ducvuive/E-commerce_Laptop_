using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackendAPI.Models
{
    public partial class InvoiceDetail
    {
        public int HoaDonId { get; set; }
        public int SanPhamId { get; set; }

        [Required]
        public int? SoLuong { get; set; }

        public virtual Invoice HoaDon { get; set; }
        public virtual Product SanPham { get; set; }
    }
}

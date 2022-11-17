using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class InvoiceDetailDTO
    {
        [Key]
        public int HoaDonId { get; set; }
        public int SanPhamId { get; set; }
        public int? SoLuong { get; set; }
        /*        public virtual HoaDonDTO HoaDon { get; set; }
                public virtual SanPhamDTO SanPham { get; set; }*/
    }
}

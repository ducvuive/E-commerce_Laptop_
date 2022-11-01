using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class CTHD_DTO
    {
        [Key]
        public int HoaDonId { get; set; }
        public int SanPhamId { get; set; }
        public int? SoLuong { get; set; }
        /*        public virtual HoaDonDTO HoaDon { get; set; }
                public virtual SanPhamDTO SanPham { get; set; }*/
    }
}

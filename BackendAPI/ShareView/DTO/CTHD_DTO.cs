using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareView.DTO
{
    public class CTHD_DTO
    {
        [Key]
        public int HoaDonId { get; set; }
        public int SanPhamId { get; set; }
        public int? SoLuong { get; set; }
        public virtual HoaDonDTO HoaDon { get; set; }
        public virtual SanPhamDTO SanPham { get; set; }
    }
}

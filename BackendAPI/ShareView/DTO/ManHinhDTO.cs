using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareView.DTO
{
    public class ManHinhDTO
    {
        [Key]
        public int ManHinhId { get; set; }
        public string? KichThuoc { get; set; }
        public string? DoPhanGiai { get; set; }
        public string? TanSoQuet { get; set; }
        public string? CongNgheMH { get; set; }
        public string? CamUng { get; set; }

        public virtual List<SanPhamDTO> Sanpham { get; set; } = new List<SanPhamDTO>();
    }
}

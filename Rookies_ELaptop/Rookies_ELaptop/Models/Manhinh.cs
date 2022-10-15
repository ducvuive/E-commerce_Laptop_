using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Rookies_ELaptop.Models
{
    public partial class ManHinh
    {
        [Key]
        public int ManHinhId { get; set; }
        public string? KichThuoc { get; set; }
        public string? DoPhanGiai { get; set; }
        public string? TanSoQuet { get; set; }
        public string? CongNgheMH { get; set; }
        public string? CamUng { get; set; }

        public virtual List<SanPham> Sanpham { get; set; } = new List<SanPham>();
    }
}

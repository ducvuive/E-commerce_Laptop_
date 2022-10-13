using Rookies_NguyenVuVanDuc_ELaptop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Test.Models
{
    public partial class BoXuLy
    {
        [Key]
        public int BoXuLyId { get; set; }
        public string CongNgheCPU { get; set; }
        public int SoNhan { get; set; } = default;
        public int SoLuong { get; set; } = default;
        public string TocDoCPU { get; set; } 
        public string ToCDoToiDa { get; set; } 
        public string BoNhoDem { get; set; } 

        public virtual List<SanPham> SanPham { get; set; } = new List<SanPham>();
    }
}

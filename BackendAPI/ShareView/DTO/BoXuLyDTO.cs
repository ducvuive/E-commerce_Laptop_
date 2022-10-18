using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ShareView.DTO
{
    public partial class BoXuLyDTO
    {
        [Key]
        public int BoXuLyId { get; set; }
        public string? CongNgheCPU { get; set; }
        public int? SoNhan { get; set; } = default;
        public int? SoLuong { get; set; } = default;
        public string? TocDoCPU { get; set; } 
        public string? ToCDoToiDa { get; set; }
        public string? BoNhoDem { get; set; } 

        public virtual List<SanPhamDTO> SanPham { get; set; } = new List<SanPhamDTO>();
    }
}

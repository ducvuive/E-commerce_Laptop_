﻿using BackendAPI.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackendAPI.Models
{
    public partial class HoaDon
    {
        [Key]
        public int HoaDonId { get; set; }
        [Required]
        public DateTime NgayHD { get; set; }

        [MaxLength(100)]
        public string? NguoiNhan { get; set; }
        public string? SDT { get; set; }

        [MaxLength(100)]
        public string DiaChiGiaoHang { get; set; }

        [Required]
        public long TongTien { get; set; }
        public int? TrangThai { get; set; }
        public virtual UserIdentity? MaKhacHangId { get; set; }
        public virtual List<CTHD> CTHD { get; set; } = new List<CTHD>();
    }
}

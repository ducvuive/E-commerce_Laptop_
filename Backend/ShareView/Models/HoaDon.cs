using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ShareView.Models 
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
        public virtual IdentityUser? MaKhacHangId { get; set; }
        public virtual List<CTHD> CTHD { get; set; } = new List<CTHD>();
    }
}

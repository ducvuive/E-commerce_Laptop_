using MessagePack;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Rookies_ELaptop.Models 
{ 
    public partial class HoaDon
    {
        public int HoaDonId { get; set; }
        [Required]
        public DateTime NgayHD { get; set; }
        public string? NguoiNhan { get; set; }
        public string? SDT { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public long TongTien { get; set; }
        public int? TrangThai { get; set; }
        public virtual IdentityUser? MaKhacHang { get; set; }
        public virtual List<CTHD> CTHD { get; set; } = new List<CTHD>();
    }
}

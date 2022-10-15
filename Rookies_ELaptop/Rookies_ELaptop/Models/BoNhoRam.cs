using Rookies_NguyenVuVanDuc_ELaptop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Rookies_ELaptop.Models
{
    public partial class BoNhoRam
    {
        [Key]
        public int RamId { get; set; }
        public string? DungLuongRam { get; set; }
        public string? LoaiRam { get; set; }
        public string? BusRam { get; set; }
        public string? HoTroToiDa { get; set; }

        public virtual List<SanPham> SanPham { get; set; } = new List<SanPham>();
    }
}

using MessagePack;
using Rookies_NguyenVuVanDuc_ELaptop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Test.Models
{
    public partial class CTHD
    {
        public int MaHD { get; set; }
        public string MaSP { get; set; }
        public int SoLuong { get; set; }

        public virtual HoaDon HoaDon { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}

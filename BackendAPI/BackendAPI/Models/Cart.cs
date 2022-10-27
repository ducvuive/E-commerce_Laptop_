﻿
// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackendAPI.Models
{
    public partial class Cart
    {
        public SanPham Sanpham { get; set; }
        public int SL { get; set; }
    }
}

using Rookies_NguyenVuVanDuc_ELaptop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Rookies_ELaptop.Models
{
    public partial class DanhMucSanPham
    {
        [Key]
        public int DMSPId { get; set; }
        [Required]
        public string TenDM { get; set; }

        public virtual List<SanPham> SanPhams { get; set; } = new List<SanPham>();
    }
}

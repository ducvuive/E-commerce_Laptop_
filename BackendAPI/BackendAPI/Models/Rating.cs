using BackendAPI.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackendAPI.Models
{
    public partial class Rating
    {
        [Key]
        public int RatingID { get; set; }
        public int? Rate { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Comments { get; set; }
        [ForeignKey("SanPhamId")]
        public virtual Product sanPham { get; set; }

        [ForeignKey("KhachHangId")]
        public virtual UserIdentity KhachHang { get; set; }
    }
}

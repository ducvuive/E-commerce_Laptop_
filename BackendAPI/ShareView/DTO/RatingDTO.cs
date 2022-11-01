using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ShareView.DTO
{
    public partial class RatingDTO
    {
        [Key]
        public int RatingID { get; set; }
        public int? Rate { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Comments { get; set; }
        public int sanPhamId { get; set; }
        public UserIdentityDTO? KhachHang { get; set; }
    }
}

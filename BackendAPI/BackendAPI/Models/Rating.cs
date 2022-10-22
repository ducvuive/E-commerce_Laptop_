using System.ComponentModel.DataAnnotations;

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

        public virtual SanPham sanPham { get; set; }
    }
}

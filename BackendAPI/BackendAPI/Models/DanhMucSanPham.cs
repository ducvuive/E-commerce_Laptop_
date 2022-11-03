using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackendAPI.Models
{
    public partial class DanhMucSanPham
    {
        [Key]
        public int DMSPId { get; set; }
        [Required]
        public string TenDM { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }

        public virtual List<SanPham> SanPhams { get; set; } = new List<SanPham>();
    }
}

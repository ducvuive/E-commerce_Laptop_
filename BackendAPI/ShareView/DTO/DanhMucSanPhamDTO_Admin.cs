using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class DanhMucSanPhamDTO_Admin
    {
        [Key]
        public int DMSPId { get; set; }
        public string TenDM { get; set; }
        public string? Description { get; set; }

        //public virtual List<SanPhamDTO> SanPhams { get; set; } = new List<SanPhamDTO>();
    }
}

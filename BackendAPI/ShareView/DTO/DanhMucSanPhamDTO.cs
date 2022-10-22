using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class DanhMucSanPhamDTO
    {
        [Key]
        public int DMSPId { get; set; }
        public string TenDM { get; set; }

        //public virtual List<SanPhamDTO> SanPhams { get; set; } = new List<SanPhamDTO>();
    }
}

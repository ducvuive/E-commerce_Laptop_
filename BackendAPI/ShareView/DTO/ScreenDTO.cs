using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class ScreenDTO
    {
        [Key]
        public int ScreenId { get; set; }
        public string? Size { get; set; }

        //public virtual List<SanPhamDTO> Sanpham { get; set; } = new List<SanPhamDTO>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class ScreenDTO
    {
        [Key]
        public int ScreenId { get; set; }
        public string? Size { get; set; }

        //public virtual List<ProductDTO> Sanpham { get; set; } = new List<ProductDTO>();
    }
}

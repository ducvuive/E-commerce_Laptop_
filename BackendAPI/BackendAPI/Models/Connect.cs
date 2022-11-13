using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models
{
    public class Connect
    {
        [Key]
        public int CongKetNoiId { get; set; }

        [MaxLength(50)]
        public string? CongGiaoTiep { get; set; }
        [MaxLength(50)]
        public string? KetNoiKhongDay { get; set; }

        [MaxLength(50)]
        public string? KheDocTheNho { get; set; }

        [MaxLength(50)]
        public string? Webcam { get; set; }

        [MaxLength(50)]
        public string? TinhNangKhac { set; get; }

        [MaxLength(50)]
        public string? DenBanPhim { set; get; }

        public virtual List<Product> Sanpham { get; set; } = new List<Product>();


    }
}

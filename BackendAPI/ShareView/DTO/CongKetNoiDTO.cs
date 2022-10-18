using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class CongKetNoiDTO
    {
        [Key]
        public int CongKetNoiId { get; set; }
        public string? CongGiaoTiep { get; set; }
        public string? KetNoiKhongDay { get; set; }
        public string? KheDocTheNho { get; set; }
        public string? Webcam {  get; set; }
        public string? TinhNangKhac { set; get; }
        public string? DenBanPhim { set; get; }

        public virtual List<SanPhamDTO> Sanphams { get; set; } = new List<SanPhamDTO>();


    }
}

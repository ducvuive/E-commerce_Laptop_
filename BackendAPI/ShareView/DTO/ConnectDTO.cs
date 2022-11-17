using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class ConnectDTO
    {
        [Key]
        public int CongKetNoiId { get; set; }
        public string? CongGiaoTiep { get; set; }
        public string? KetNoiKhongDay { get; set; }
        public string? KheDocTheNho { get; set; }
        public string? Webcam { get; set; }
        public string? TinhNangKhac { set; get; }
        public string? DenBanPhim { set; get; }


    }
}

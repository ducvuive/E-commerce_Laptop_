using MessagePack;
using System.Security.Cryptography.X509Certificates;

namespace Rookies_ELaptop.Models
{
    public class CongKetNoi
    {
        public int CongKetNoiId { get; set; }
        public string? CongGiaoTiep { get; set; }
        public string? KetNoiKhongDay { get; set; }
        public string? KheDocTheNho { get; set; }
        public string? Webcam {  get; set; }
        public string? TinhNangKhac { set; get; }
        public string? DenBanPhim { set; get; }

        public virtual List<SanPham> Sanphams { get; set; } = new List<SanPham>();


    }
}

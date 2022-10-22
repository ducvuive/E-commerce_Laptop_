using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models
{
    public class SanPham
    {
        [Key]
        public int SanPhamId { get; set; }
        public int ManHinhId { get; set; }
        public int BoXuLyId { get; set; }
        public int RamId { get; set; }
        public int CongKetNoiId { get; set; }
        public int DMSPId { get; set; }
        public string? TenSP { get; set; }
        public int? SoLuong { get; set; }
        public string? MauSac { get; set; }
        public string? OCung { get; set; }
        public string? CardManHinh { get; set; }
        public string? DacBiet { get; set; }
        public string? HDH { get; set; }
        public string? ThietKe { get; set; }
        public string? KichThuocTrongLuong { get; set; }
        public string? Webcam { get; set; }
        public string? Pin { get; set; }
        public int? RaMat { get; set; }
        public string MoTa { get; set; }
        [Required]
        public long DonGia { get; set; }
        public string? HinhAnh { get; set; }
        public float? DanhGia { get; set; }

        public virtual ManHinh MH { get; set; }
        public virtual BoXuLy BXL { get; set; }
        public virtual BoNhoRam Ram { get; set; }
        public virtual CongKetNoi CongKN { get; set; }
        public virtual DanhMucSanPham DMSP { get; set; }
        public virtual List<CTHD> CTHD { get; set; } = new List<CTHD>();
    }
}

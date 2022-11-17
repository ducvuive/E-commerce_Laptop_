using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class InvoiceDTO
    {
        [Key]
        public int HoaDonId { get; set; }
        public DateTime? NgayHD { get; set; }
        public string? NguoiNhan { get; set; }
        public string? SDT { get; set; }
        public string? DiaChiGiaoHang { get; set; }
        public long? TongTien { get; set; }
        public int? TrangThai { get; set; }
        public UserIdentityDTO? KhacHangId { get; set; }
        //public virtual UserIdentityDTO? MaKhacHangId { get; set; }
        //public virtual List<CTHD_DTO> CTHD { get; set; } = new List<CTHD_DTO>();
    }
}

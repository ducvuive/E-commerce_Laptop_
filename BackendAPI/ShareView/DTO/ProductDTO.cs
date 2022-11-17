namespace ShareView.DTO
{
    public class ProductDTO
    {
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
        public long DonGia { get; set; }
        public string? HinhAnh { get; set; }
        public float? DanhGia { get; set; }
        public List<RatingDTO> Rating { get; set; }
    }
}

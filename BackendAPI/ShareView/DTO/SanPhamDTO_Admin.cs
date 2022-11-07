namespace ShareView.DTO
{
    public class SanPhamDTO_Admin
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
        public string? MoTa { get; set; }
        public long? DonGia { get; set; }
        public string? HinhAnh { get; set; }
        public float? DanhGia { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        //public List<RatingDTO> Rating { get; set; }

        /*        public virtual ManHinhDTO MH { get; set; }
                public virtual BoXuLyDTO BXL { get; set; }
                public virtual BoNhoRamDTO Ram { get; set; }
                public virtual CongKetNoiDTO CongKN { get; set; }
                public virtual DanhMucSanPhamDTO DMSP { get; set; }*/

        //public virtual List<CTHD_DTO> CTHD { get; set; } = new List<CTHD_DTO>();
    }
}

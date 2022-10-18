using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareView.DTO
{
    public class SanPhamDTO
    {
        [Key]
        public int SanPhamId { get; set; }
        public int ManHinhId { get; set; }
        public int BoXuLyId { get; set; }
        public int RamId { get; set; }
        public int CongKetNoiId { get; set; }
        public int DMSPId { get; set; }
        /*public string? TenSP { get; set; }
        public int? SoLuong { get; set; }
        public int? RaMat { get; set; }
        public string MoTa { get; set; }
        public long DonGia { get; set; }
        public string? HinhAnh { get; set; }*/

        public virtual ManHinhDTO MH { get; set; }
        public virtual BoXuLyDTO BXL { get; set; }
        public virtual BoNhoRamDTO Ram { get; set; }
        public virtual CongKetNoiDTO CongKN { get; set; }
        public virtual DanhMucSanPhamDTO DMSP { get; set; }

        public virtual List<CTHD_DTO> CTHD { get; set; } = new List<CTHD_DTO>();
    }
}

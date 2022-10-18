﻿using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class BoNhoRamDTO
    {
        public int RamId { get; set; }
        public string? DungLuongRam { get; set; }
        public string? LoaiRam { get; set; }
        public string? BusRam { get; set; }
        public string? HoTroToiDa { get; set; }
        //public virtual List<SanPhamDTO> SanPham { get; set; } = new List<SanPhamDTO>();
    }
}

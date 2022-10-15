using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Rookies_NguyenVuVanDuc_ELaptop.Models;
using System.Reflection.Emit;
using Test.Models;

namespace Rookies_NguyenVuVanDuc_ELaptop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public virtual DbSet<BoNhoRam> Bonhoram { get; set; }
        public virtual DbSet<BoXuLy> Boxuly { get; set; }
        public virtual DbSet<CongKetNoi> Congketnoi { get; set; }
        public virtual DbSet<CTHD> Cthd { get; set; }
        public virtual DbSet<DanhMucSanPham> Danhmucsanpham { get; set; }
        public virtual DbSet<HoaDon> Hoadon { get; set; }
        public virtual DbSet<ManHinh> Manhinh { get; set; }
        public virtual DbSet<SanPham> Sanpham { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BoNhoRam>()
                .HasKey(t => t.RamId);
            modelBuilder.Entity<BoXuLy>()
               .HasKey(t => t.BoXuLyId);
            modelBuilder.Entity<CongKetNoi>()
               .HasKey(t => t.CongKetNoiId);
            modelBuilder.Entity<CTHD>()
               .HasKey(t => new { t.MaHD, t.MaSP });
            modelBuilder.Entity<DanhMucSanPham>()
               .HasKey(t => t.DMSPId);
            modelBuilder.Entity<HoaDon>()
               .HasKey(t => t.HoaDonId);
            modelBuilder.Entity<ManHinh>()
               .HasKey(t => t.ManHinhId);
            modelBuilder.Entity<SanPham>()
               .HasKey(t => t.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
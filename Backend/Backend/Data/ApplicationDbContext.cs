using Backend.Data.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShareView.Models;

namespace Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<BoNhoRam> BoNhoRam { get; set; }
        public virtual DbSet<BoXuLy> BoXuLy { get; set; }
        public virtual DbSet<CongKetNoi> CongKetNoi { get; set; }
        public virtual DbSet<CTHD> CTHD { get; set; }
        public virtual DbSet<DanhMucSanPham> DanhMucSanPham { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<ManHinh> ManHinh { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BoNhoRamConfig());

            modelBuilder.ApplyConfiguration(new BoXuLyConfig());

            modelBuilder.ApplyConfiguration(new CongKetNoiConfig());

            modelBuilder.ApplyConfiguration(new CTHDConfig());

            modelBuilder.ApplyConfiguration(new DanhMucSanPhamConfig());

            modelBuilder.ApplyConfiguration(new HoaDonConfig());

            modelBuilder.ApplyConfiguration(new ManHinhConfig());

            modelBuilder.ApplyConfiguration(new SanPhamConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
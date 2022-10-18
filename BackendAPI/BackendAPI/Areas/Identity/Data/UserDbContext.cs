using BackendAPI.Areas.Identity.Config;
using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BackendAPI.Areas.Identity.Data;

public class UserDbContext : IdentityDbContext<UserIdentity>
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
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
    public virtual DbSet<UserIdentity> AppUser { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfiguration(new BoNhoRamConfig());

        builder.ApplyConfiguration(new BoXuLyConfig());

        builder.ApplyConfiguration(new CongKetNoiConfig());

        builder.ApplyConfiguration(new CTHDConfig());

        builder.ApplyConfiguration(new DanhMucSanPhamConfig());

        builder.ApplyConfiguration(new HoaDonConfig());

        builder.ApplyConfiguration(new ManHinhConfig());

        builder.ApplyConfiguration(new SanPhamConfig());

        base.OnModelCreating(builder);
        
    }
}

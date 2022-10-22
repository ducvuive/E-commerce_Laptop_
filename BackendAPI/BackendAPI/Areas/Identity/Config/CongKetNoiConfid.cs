using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BackendAPI.Areas.Identity.Config
{
    public class CongKetNoiConfig : IEntityTypeConfiguration<CongKetNoi>
    {
        public void Configure(EntityTypeBuilder<CongKetNoi> builder)
        {

            builder.HasKey(o => o.CongKetNoiId);

            builder.Property(o => o.CongGiaoTiep)
           .HasMaxLength(200);

            builder.Property(o => o.DenBanPhim)
           .HasMaxLength(20);

            builder.Property(o => o.KetNoiKhongDay)
           .HasMaxLength(100);

            builder.Property(o => o.KheDocTheNho)
           .HasMaxLength(20);

            builder.Property(o => o.TinhNangKhac)
           .HasMaxLength(200);

            builder.Property(o => o.Webcam)
           .HasMaxLength(50);

        }
    }
}

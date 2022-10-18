using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendAPI.Models;

namespace BackendAPI.Areas.Identity.Config
{
    public class HoaDonConfig : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.ToTable("HoaDon");

            builder.HasKey(o => o.HoaDonId);

            builder.Property(o => o.DiaChiGiaoHang)
           .IsRequired();

        }
    }
}

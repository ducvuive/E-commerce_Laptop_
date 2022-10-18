using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendAPI.Models;

namespace BackendAPI.Areas.Identity.Config
{
    public class ManHinhConfig : IEntityTypeConfiguration<ManHinh>
    {
        public void Configure(EntityTypeBuilder<ManHinh> builder)
        {
            builder.ToTable("ManHinh");

            builder.HasKey(o => o.ManHinhId);

            builder.Property(o => o.TanSoQuet)
           .HasMaxLength(20);

            builder.Property(o => o.DoPhanGiai)
           .HasMaxLength(40);

            builder.Property(o => o.CamUng)
           .HasMaxLength(10);

            builder.Property(o => o.CongNgheMH)
           .HasMaxLength(100);

            builder.Property(o => o.KichThuoc)
           .HasMaxLength(20);

            builder.Property(o => o.TanSoQuet)
           .HasMaxLength(10);

        }
    }
}

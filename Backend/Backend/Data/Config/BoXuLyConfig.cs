using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareView.Models;

namespace Backend.Data.Config
{
    public class BoXuLyConfig : IEntityTypeConfiguration<BoXuLy>
    {
        public void Configure(EntityTypeBuilder<BoXuLy> builder)
        {
            builder.ToTable("BoXuLy");

            builder.HasKey(o => o.BoXuLyId);

            builder.Property(o => o.BoNhoDem)
           .HasMaxLength(20);

            builder.Property(o => o.CongNgheCPU)
           .HasMaxLength(50);

            builder.Property(o => o.SoLuong)
           .HasMaxLength(10)
           .HasColumnName("SOLUONG");


            builder.Property(o => o.SoNhan)
           .HasMaxLength(20);

            builder.Property(o => o.TocDoCPU)
           .HasMaxLength(20);

            builder.Property(o => o.ToCDoToiDa)
           .HasMaxLength(50);

        }
    }
}

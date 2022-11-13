using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Areas.Identity.Config
{
    public class ProcessorConfig : IEntityTypeConfiguration<Processor>
    {
        public void Configure(EntityTypeBuilder<Processor> builder)
        {
            builder.ToTable("Processor");

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

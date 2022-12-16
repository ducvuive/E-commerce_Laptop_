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

            builder.HasKey(o => o.ProcessorId);

            builder.Property(o => o.Cache)
           .HasMaxLength(20);

            builder.Property(o => o.CPUTechnology)
           .HasMaxLength(50);

            builder.Property(o => o.Multiplier)
           .HasMaxLength(10);
            /*           .HasColumnName("SOLUONG");*/


            builder.Property(o => o.Thread)
           .HasMaxLength(20);

            builder.Property(o => o.Speed)
           .HasMaxLength(20);

            builder.Property(o => o.MaxSpeed)
           .HasMaxLength(50);

        }
    }
}

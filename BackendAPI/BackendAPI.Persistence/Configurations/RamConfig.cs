using BackendAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Persistence.Configurations
{
    public class RamConfig : IEntityTypeConfiguration<Ram>
    {
        public void Configure(EntityTypeBuilder<Ram> builder)
        {
            builder.ToTable("Ram");

            builder.HasKey(o => o.RamId);

            builder.Property(o => o.BusRam)
            .HasMaxLength(100);

            builder.Property(o => o.Capacity)
            .HasMaxLength(100);
        }
    }
}

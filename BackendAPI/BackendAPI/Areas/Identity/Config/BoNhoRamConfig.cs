using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Areas.Identity.Config
{
    public class BoNhoRamConfig : IEntityTypeConfiguration<BoNhoRam>
    {
        public void Configure(EntityTypeBuilder<BoNhoRam> builder)
        {
            builder.ToTable("BoNhoRam");

            builder.HasKey(o => o.RamId);

            builder.Property(o => o.BusRam)
            .HasMaxLength(100);

            builder.Property(o => o.DungLuongRam)
            .HasMaxLength(100);
        }
    }
}

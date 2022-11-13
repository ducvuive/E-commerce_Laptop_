using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Areas.Identity.Config
{
    public class RamConfig : IEntityTypeConfiguration<Ram>
    {
        public void Configure(EntityTypeBuilder<Ram> builder)
        {
            builder.ToTable("Ram");

            builder.HasKey(o => o.RamId);

            builder.Property(o => o.BusRam)
            .HasMaxLength(100);

            builder.Property(o => o.DungLuongRam)
            .HasMaxLength(100);
        }
    }
}

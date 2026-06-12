using BackendAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Persistence.Configurations
{
    public class ScreenConfig : IEntityTypeConfiguration<Screen>
    {
        public void Configure(EntityTypeBuilder<Screen> builder)
        {
            builder.ToTable("Screen");

            builder.HasKey(o => o.ScreenId);

            builder.Property(o => o.Size)
           .HasMaxLength(100);

            // builder.Property(o => o.DoPhanGiai)
            //.HasMaxLength(40);

            // builder.Property(o => o.CamUng)
            //.HasMaxLength(10);

            // builder.Property(o => o.CongNgheMH)
            //.HasMaxLength(100);

            // builder.Property(o => o.KichThuoc)
            //.HasMaxLength(20);

            // builder.Property(o => o.TanSoQuet)
            //.HasMaxLength(10);

        }
    }
}

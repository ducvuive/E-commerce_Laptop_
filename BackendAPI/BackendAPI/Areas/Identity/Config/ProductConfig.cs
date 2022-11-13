using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Areas.Identity.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(o => o.SanPhamId);

            builder.Property(o => o.CardManHinh)
            .HasMaxLength(100);

            builder.Property(o => o.DacBiet)
            .HasMaxLength(100);

            builder.Property(o => o.Pin)
            .HasMaxLength(40);

            builder.Property(o => o.KichThuocTrongLuong)
            .HasMaxLength(100);

        }
    }
}

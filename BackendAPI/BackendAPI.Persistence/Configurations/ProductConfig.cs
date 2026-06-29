using BackendAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Persistence.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(o => o.ProductId);

            builder.Property(o => o.Quantity)
                .IsRequired()
                .HasDefaultValue(0);

            //builder.Property(o => o.CardScreen)
            //.HasMaxLength(100);

            //builder.Property(o => o.DacBiet)
            //.HasMaxLength(100);

            //builder.Property(o => o.Pin)
            //.HasMaxLength(40);

            //builder.Property(o => o.KichThuocTrongLuong)
            //.HasMaxLength(100);

        }
    }
}

using BackendAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Persistence.Configurations
{
    public class CategoriesConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(o => o.CategoryId);

            builder.Property(o => o.Name)
           .IsRequired()
           .HasMaxLength(50);

        }
    }
}

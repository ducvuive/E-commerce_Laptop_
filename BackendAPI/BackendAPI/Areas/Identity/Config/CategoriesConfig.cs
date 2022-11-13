using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Areas.Identity.Config
{
    public class CategoriesConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(o => o.DMSPId);

            builder.Property(o => o.TenDM)
           .IsRequired()
           .HasMaxLength(50);

        }
    }
}

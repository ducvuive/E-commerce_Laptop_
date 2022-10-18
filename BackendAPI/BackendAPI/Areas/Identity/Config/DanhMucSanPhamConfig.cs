using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendAPI.Models;

namespace BackendAPI.Areas.Identity.Config
{
    public class DanhMucSanPhamConfig : IEntityTypeConfiguration<DanhMucSanPham>
    {
        public void Configure(EntityTypeBuilder<DanhMucSanPham> builder)
        {
            builder.ToTable("DanhMucSanPham");

            builder.HasKey(o => o.DMSPId);

            builder.Property(o => o.TenDM)
           .IsRequired()
           .HasMaxLength(50);

        }
    }
}

using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Areas.Identity.Config
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoice");

            builder.HasKey(o => o.HoaDonId);

            builder.Property(o => o.DiaChiGiaoHang)
           .IsRequired();

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendAPI.Models;

namespace BackendAPI.Areas.Identity.Config
{
    public class CTHDConfig : IEntityTypeConfiguration<CTHD>
    {
        public void Configure(EntityTypeBuilder<CTHD> builder)
        {
            builder.ToTable("CTHD");

            builder.HasKey(o => new { o.SanPhamId, o.HoaDonId });

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rookies_ELaptop.Models;

namespace Rookies_ELaptop.Data.Config
{
    public class CTHDConfig : IEntityTypeConfiguration<CTHD>
    {
        public void Configure(EntityTypeBuilder<CTHD> builder)
        {
            builder.ToTable("CTHD");

            builder.HasKey(o =>new { o.SanPhamId, o.HoaDonId});

        }
    }
}

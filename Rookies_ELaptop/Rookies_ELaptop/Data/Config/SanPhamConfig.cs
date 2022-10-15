﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rookies_ELaptop.Models;

namespace Rookies_ELaptop.Data.Config
{
    public class SanPhamConfig : IEntityTypeConfiguration<SanPham>
    {
        public void Configure(EntityTypeBuilder<SanPham> builder)
        {
            builder.ToTable("SanPham");

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

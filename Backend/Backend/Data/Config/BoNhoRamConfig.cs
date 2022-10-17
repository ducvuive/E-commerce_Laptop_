﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareView.Models;

namespace Backend.Data.Config
{
    public class BoNhoRamConfig : IEntityTypeConfiguration<BoNhoRam>
    {
        public void Configure(EntityTypeBuilder<BoNhoRam> builder)
        {
            builder.ToTable("BoNhoRam");

            builder.HasKey(o => o.RamId);

            builder.Property(o => o.BusRam)
            .HasMaxLength(100);

            builder.Property(o => o.DungLuongRam)
            .HasMaxLength(100);
        }
    }
}

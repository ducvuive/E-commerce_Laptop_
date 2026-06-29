using BackendAPI.Domain.Entities;
using BackendAPI.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Persistence.Configurations
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoice");

            builder.HasKey(o => o.InvoiceId);

            builder.Property(o => o.Address)
           .IsRequired();

            builder.Property(o => o.IdempotencyKey)
                .HasMaxLength(80);

            builder.HasIndex(o => new { o.CustomerId, o.IdempotencyKey })
                .IsUnique()
                .HasFilter("[IdempotencyKey] IS NOT NULL");

            builder.HasOne<UserIdentity>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId);

        }
    }
}

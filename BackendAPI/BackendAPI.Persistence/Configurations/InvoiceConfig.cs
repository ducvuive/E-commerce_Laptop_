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

            builder.HasOne<UserIdentity>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId);

        }
    }
}

using BackendAPI.Domain.Entities;
using BackendAPI.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Persistence.Configurations;

public class RatingConfig : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasOne<UserIdentity>()
            .WithMany()
            .HasForeignKey(rating => rating.CustomerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}

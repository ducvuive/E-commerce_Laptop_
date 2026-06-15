using BackendAPI.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Persistence.Configurations;

public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(refreshToken => refreshToken.RefreshTokenId);

        builder.Property(refreshToken => refreshToken.UserId)
            .HasMaxLength(450)
            .IsRequired();

        builder.Property(refreshToken => refreshToken.SessionId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(refreshToken => refreshToken.TokenHash)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(refreshToken => refreshToken.UserAgent)
            .HasMaxLength(512);

        builder.Property(refreshToken => refreshToken.IpAddress)
            .HasMaxLength(64);

        builder.HasIndex(refreshToken => new { refreshToken.UserId, refreshToken.SessionId })
            .IsUnique();

        builder.HasIndex(refreshToken => refreshToken.ExpiresAtUtc);

        builder.HasIndex(refreshToken => refreshToken.RevokedAtUtc);

        builder.HasOne(refreshToken => refreshToken.User)
            .WithMany()
            .HasForeignKey(refreshToken => refreshToken.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

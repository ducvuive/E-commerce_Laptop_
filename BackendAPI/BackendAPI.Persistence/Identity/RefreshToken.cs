using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Persistence.Identity;

public class RefreshToken
{
    public int RefreshTokenId { get; set; }

    [MaxLength(450)]
    public string UserId { get; set; } = string.Empty;

    [MaxLength(64)]
    public string SessionId { get; set; } = string.Empty;

    [MaxLength(128)]
    public string TokenHash { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }

    public DateTime ExpiresAtUtc { get; set; }

    public DateTime? LastUsedAtUtc { get; set; }

    public DateTime? RevokedAtUtc { get; set; }

    [MaxLength(512)]
    public string? UserAgent { get; set; }

    [MaxLength(64)]
    public string? IpAddress { get; set; }

    public UserIdentity User { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ShareView.DTO
{
    public class LoginResponseModel
    {
        public string Value { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiresAtUtc { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiresAtUtc { get; set; }
        public string RefreshSessionId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; }
    }

    public class RefreshTokenRequestModel
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string RefreshToken { get; set; } = string.Empty;

        [Required]
        public string RefreshSessionId { get; set; } = string.Empty;
    }
}

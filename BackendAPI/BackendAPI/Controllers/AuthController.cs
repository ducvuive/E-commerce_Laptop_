using BackendAPI.Persistence.Identity;
using BackendAPI.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShareView.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BackendAPI.Controllers
{
    /*    [route("[controller]")]
        [apicontroller]*/
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly TimeSpan DefaultAccessTokenLifetime = TimeSpan.FromMinutes(30);
        private static readonly TimeSpan DefaultRefreshTokenLifetime = TimeSpan.FromDays(7);

        private readonly IConfiguration config;
        private readonly UserManager<UserIdentity> userManager;
        private readonly UserDbContext userDbContext;

        private TimeSpan AccessTokenLifetime =>
            GetConfiguredLifetime("RefreshTokens:AccessTokenLifetimeMinutes", DefaultAccessTokenLifetime, TimeSpan.FromMinutes);

        private TimeSpan RefreshTokenLifetime =>
            GetConfiguredLifetime("RefreshTokens:RefreshTokenLifetimeDays", DefaultRefreshTokenLifetime, TimeSpan.FromDays);

        public AuthController(IConfiguration config, UserManager<UserIdentity> userManager, UserDbContext userDbContext)
        {
            this.config = config;
            this.userManager = userManager;
            this.userDbContext = userDbContext;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IResult> Register([FromBody] RegisterRequestModel registerRequestModel)
        {
            if (ModelState.IsValid)
            {
                var user = new UserIdentity
                {
                    UserName = registerRequestModel.UserName,
                    Email = registerRequestModel.UserName,
                    PhoneNumber = registerRequestModel.PhoneNumber,
                };

                var createUserResult = await userManager.CreateAsync(user, registerRequestModel.Password);

                if (createUserResult.Succeeded)
                {
                    return Results.Ok();
                }

                return Results.BadRequest(createUserResult.Errors.Select(error => error.Description));
            }

            return Results.BadRequest(ModelState.Values.SelectMany(x => x.Errors));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IResult> Login([FromBody] LoginRequestModel loginRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }

            var identityUser = await userManager.FindByEmailAsync(loginRequestModel.UserName);
            if (identityUser == null || !await userManager.CheckPasswordAsync(identityUser, loginRequestModel.Password))
            {
                return Results.Unauthorized();
            }

            var response = await BuildLoginResponse(identityUser);
            return Results.Ok(response);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IResult> Refresh([FromBody] RefreshTokenRequestModel refreshTokenRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }

            var identityUser = await userManager.FindByIdAsync(refreshTokenRequestModel.UserId);
            if (identityUser == null)
            {
                return Results.Unauthorized();
            }

            var refreshSessionId = refreshTokenRequestModel.RefreshSessionId?.Trim() ?? string.Empty;
            if (!IsValidRefreshSessionId(refreshSessionId))
            {
                return Results.Unauthorized();
            }

            var refreshToken = await userDbContext.RefreshTokens
                .SingleOrDefaultAsync(token =>
                    token.UserId == identityUser.Id &&
                    token.SessionId == refreshSessionId);

            if (!IsRefreshTokenValid(refreshToken, refreshTokenRequestModel.RefreshToken))
            {
                return Results.Unauthorized();
            }

            var response = await BuildLoginResponse(identityUser, refreshToken);
            return Results.Ok(response);
        }

        [HttpPost]
        [Route("revoke")]
        public async Task<IResult> Revoke([FromBody] RefreshTokenRequestModel refreshTokenRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }

            var identityUser = await userManager.FindByIdAsync(refreshTokenRequestModel.UserId);
            var refreshSessionId = refreshTokenRequestModel.RefreshSessionId?.Trim() ?? string.Empty;
            if (identityUser == null || !IsValidRefreshSessionId(refreshSessionId))
            {
                return Results.Unauthorized();
            }

            var refreshToken = await userDbContext.RefreshTokens
                .SingleOrDefaultAsync(token =>
                    token.UserId == identityUser.Id &&
                    token.SessionId == refreshSessionId);

            if (!IsRefreshTokenValid(refreshToken, refreshTokenRequestModel.RefreshToken))
            {
                return Results.Unauthorized();
            }

            refreshToken!.RevokedAtUtc = DateTime.UtcNow;
            await userDbContext.SaveChangesAsync();

            return Results.NoContent();
        }

        private async Task<LoginResponseModel> BuildLoginResponse(UserIdentity identityUser, RefreshToken? refreshTokenRecord = null)
        {
            var now = DateTime.UtcNow;
            var expiresAtUtc = DateTime.UtcNow.Add(AccessTokenLifetime);
            var accessToken = await GenerateAccessToken(identityUser, expiresAtUtc);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiresAtUtc = DateTime.UtcNow.Add(RefreshTokenLifetime);
            var isNewRefreshTokenRecord = refreshTokenRecord == null;

            refreshTokenRecord ??= new RefreshToken
            {
                UserId = identityUser.Id,
                SessionId = GenerateRefreshSessionId(),
                CreatedAtUtc = now
            };

            refreshTokenRecord.TokenHash = HashToken(refreshToken);
            refreshTokenRecord.ExpiresAtUtc = refreshTokenExpiresAtUtc;
            refreshTokenRecord.RevokedAtUtc = null;
            refreshTokenRecord.UserAgent = GetRequestUserAgent();
            refreshTokenRecord.IpAddress = GetRequestIpAddress();
            if (!isNewRefreshTokenRecord)
            {
                refreshTokenRecord.LastUsedAtUtc = now;
            }

            if (isNewRefreshTokenRecord)
            {
                await userDbContext.RefreshTokens.AddAsync(refreshTokenRecord);
            }

            await userDbContext.SaveChangesAsync();

            return new LoginResponseModel
            {
                Value = accessToken,
                AccessToken = accessToken,
                ExpiresAtUtc = expiresAtUtc,
                RefreshToken = refreshToken,
                RefreshTokenExpiresAtUtc = refreshTokenExpiresAtUtc,
                RefreshSessionId = refreshTokenRecord.SessionId,
                UserId = identityUser.Id,
                StatusCode = HttpStatusCode.OK
            };
        }

        private async Task<string> GenerateAccessToken(UserIdentity identityUser, DateTime expiresAtUtc)
        {
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];
            var jwtKey = config["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");
            var key = Encoding.UTF8.GetBytes(jwtKey);
            var email = identityUser.Email ?? identityUser.UserName ?? string.Empty;
            var roles = await userManager.GetRolesAsync(identityUser);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAtUtc,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        private static string GenerateRefreshSessionId()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(16));
        }

        private static string HashToken(string token)
        {
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var hashBytes = SHA256.HashData(tokenBytes);
            return Convert.ToBase64String(hashBytes);
        }

        private static bool IsValidRefreshSessionId(string refreshSessionId)
        {
            return !string.IsNullOrWhiteSpace(refreshSessionId) &&
                refreshSessionId.Length <= 64 &&
                refreshSessionId.All(char.IsAsciiHexDigit);
        }

        private static bool IsRefreshTokenValid(RefreshToken? refreshTokenRecord, string refreshToken)
        {
            return refreshTokenRecord != null &&
                refreshTokenRecord.RevokedAtUtc == null &&
                refreshTokenRecord.ExpiresAtUtc > DateTime.UtcNow &&
                refreshTokenRecord.TokenHash == HashToken(refreshToken);
        }

        private string? GetRequestUserAgent()
        {
            return HttpContext?.Request.Headers.UserAgent.ToString();
        }

        private string? GetRequestIpAddress()
        {
            return HttpContext?.Connection.RemoteIpAddress?.ToString();
        }

        private TimeSpan GetConfiguredLifetime(string key, TimeSpan defaultValue, Func<double, TimeSpan> convert)
        {
            var value = config.GetValue<double?>(key);
            return value is > 0 ? convert(value.Value) : defaultValue;
        }

    }
}

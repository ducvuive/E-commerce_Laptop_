using BackendAPI.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShareView.DTO;
using System.Globalization;
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
        private const string RefreshTokenProvider = "BackendAPI";
        private const string RefreshTokenName = "RefreshToken";
        private const string RefreshTokenExpiresName = "RefreshTokenExpiresUtc";
        private static readonly TimeSpan AccessTokenLifetime = TimeSpan.FromMinutes(1);
        private static readonly TimeSpan RefreshTokenLifetime = TimeSpan.FromDays(7);

        private readonly IConfiguration config;
        private readonly UserManager<UserIdentity> userManager;

        public AuthController(IConfiguration config, UserManager<UserIdentity> userManager)
        {
            this.config = config;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IResult> Register([FromBody] RegisterRequestModel registerRequestModel)
        {
            //var a = 1;
            // khong bi loi require,...
            if (ModelState.IsValid)
            {
                var user = new UserIdentity
                {
                    UserName = registerRequestModel.UserName,
                    Email = registerRequestModel.UserName,
                    PhoneNumber = registerRequestModel.PhoneNumber,
                };

                // tao user trong database
                var createUserResult = await userManager.CreateAsync(user, registerRequestModel.Password);
                //a = 1;

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

            var storedRefreshTokenHash = await userManager.GetAuthenticationTokenAsync(
                identityUser,
                RefreshTokenProvider,
                RefreshTokenName);
            var storedRefreshTokenExpires = await userManager.GetAuthenticationTokenAsync(
                identityUser,
                RefreshTokenProvider,
                RefreshTokenExpiresName);

            if (string.IsNullOrWhiteSpace(storedRefreshTokenHash) ||
                storedRefreshTokenHash != HashToken(refreshTokenRequestModel.RefreshToken) ||
                !DateTime.TryParse(
                    storedRefreshTokenExpires,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AdjustToUniversal,
                    out var refreshTokenExpiresAtUtc) ||
                refreshTokenExpiresAtUtc <= DateTime.UtcNow)
            {
                await RemoveRefreshToken(identityUser);
                return Results.Unauthorized();
            }

            var response = await BuildLoginResponse(identityUser);
            return Results.Ok(response);
        }

        [HttpPost]
        [Route("revoke")]
        public async Task<IResult> Revoke([FromBody] RefreshTokenRequestModel refreshTokenRequestModel)
        {
            var identityUser = await userManager.FindByIdAsync(refreshTokenRequestModel.UserId);
            if (identityUser != null)
            {
                await RemoveRefreshToken(identityUser);
            }

            return Results.NoContent();
        }

        private async Task<LoginResponseModel> BuildLoginResponse(UserIdentity identityUser)
        {
            var expiresAtUtc = DateTime.UtcNow.Add(AccessTokenLifetime);
            var accessToken = await GenerateAccessToken(identityUser, expiresAtUtc);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiresAtUtc = DateTime.UtcNow.Add(RefreshTokenLifetime);

            await userManager.SetAuthenticationTokenAsync(
                identityUser,
                RefreshTokenProvider,
                RefreshTokenName,
                HashToken(refreshToken));
            await userManager.SetAuthenticationTokenAsync(
                identityUser,
                RefreshTokenProvider,
                RefreshTokenExpiresName,
                refreshTokenExpiresAtUtc.ToString("O", CultureInfo.InvariantCulture));

            return new LoginResponseModel
            {
                Value = accessToken,
                AccessToken = accessToken,
                ExpiresAtUtc = expiresAtUtc,
                RefreshToken = refreshToken,
                RefreshTokenExpiresAtUtc = refreshTokenExpiresAtUtc,
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

        private static string HashToken(string token)
        {
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var hashBytes = SHA256.HashData(tokenBytes);
            return Convert.ToBase64String(hashBytes);
        }

        private async Task RemoveRefreshToken(UserIdentity identityUser)
        {
            await userManager.RemoveAuthenticationTokenAsync(identityUser, RefreshTokenProvider, RefreshTokenName);
            await userManager.RemoveAuthenticationTokenAsync(identityUser, RefreshTokenProvider, RefreshTokenExpiresName);
        }

    }
}

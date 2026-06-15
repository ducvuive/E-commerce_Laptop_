using BackendAPI.Controllers;
using BackendAPI.Persistence.Data;
using BackendAPI.Persistence.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShareView.DTO;
using System.Text.Json;

namespace LaptopStore_Test.Controller
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Refresh_Should_Not_Invalidate_Other_Login_Sessions_For_Same_User()
        {
            await using var serviceProvider = BuildServiceProvider();
            var userManager = serviceProvider.GetRequiredService<UserManager<UserIdentity>>();
            var dbContext = serviceProvider.GetRequiredService<UserDbContext>();
            var controller = new AuthController(BuildConfiguration(), userManager, dbContext);
            const string email = "multi-session@example.com";
            const string password = "123";

            var createUserResult = await userManager.CreateAsync(new UserIdentity
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            }, password);
            Assert.True(createUserResult.Succeeded);

            var firstLogin = await ExecuteOk<LoginResponseModel>(
                await controller.Login(new LoginRequestModel { UserName = email, Password = password }),
                serviceProvider);
            var secondLogin = await ExecuteOk<LoginResponseModel>(
                await controller.Login(new LoginRequestModel { UserName = email, Password = password }),
                serviceProvider);

            Assert.NotEqual(firstLogin.RefreshSessionId, secondLogin.RefreshSessionId);

            var firstRefresh = await ExecuteOk<LoginResponseModel>(
                await controller.Refresh(new RefreshTokenRequestModel
                {
                    UserId = firstLogin.UserId,
                    RefreshToken = firstLogin.RefreshToken,
                    RefreshSessionId = firstLogin.RefreshSessionId
                }),
                serviceProvider);
            var secondRefresh = await ExecuteOk<LoginResponseModel>(
                await controller.Refresh(new RefreshTokenRequestModel
                {
                    UserId = secondLogin.UserId,
                    RefreshToken = secondLogin.RefreshToken,
                    RefreshSessionId = secondLogin.RefreshSessionId
                }),
                serviceProvider);

            Assert.Equal(firstLogin.RefreshSessionId, firstRefresh.RefreshSessionId);
            Assert.Equal(secondLogin.RefreshSessionId, secondRefresh.RefreshSessionId);
            Assert.Equal(2, await dbContext.RefreshTokens.CountAsync());
        }

        [Fact]
        public async Task Revoke_Should_Verify_Refresh_Token_Before_Revoking_Session()
        {
            await using var serviceProvider = BuildServiceProvider();
            var userManager = serviceProvider.GetRequiredService<UserManager<UserIdentity>>();
            var dbContext = serviceProvider.GetRequiredService<UserDbContext>();
            var controller = new AuthController(BuildConfiguration(), userManager, dbContext);
            const string email = "revoke-session@example.com";
            const string password = "123";

            var createUserResult = await userManager.CreateAsync(new UserIdentity
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            }, password);
            Assert.True(createUserResult.Succeeded);

            var login = await ExecuteOk<LoginResponseModel>(
                await controller.Login(new LoginRequestModel { UserName = email, Password = password }),
                serviceProvider);

            await ExecuteStatusCode(
                await controller.Revoke(new RefreshTokenRequestModel
                {
                    UserId = login.UserId,
                    RefreshToken = "wrong-token",
                    RefreshSessionId = login.RefreshSessionId
                }),
                serviceProvider,
                StatusCodes.Status401Unauthorized);

            var refreshTokenRecord = await dbContext.RefreshTokens.SingleAsync();
            Assert.Null(refreshTokenRecord.RevokedAtUtc);

            await ExecuteStatusCode(
                await controller.Revoke(new RefreshTokenRequestModel
                {
                    UserId = login.UserId,
                    RefreshToken = login.RefreshToken,
                    RefreshSessionId = login.RefreshSessionId
                }),
                serviceProvider,
                StatusCodes.Status204NoContent);

            refreshTokenRecord = await dbContext.RefreshTokens.SingleAsync();
            Assert.NotNull(refreshTokenRecord.RevokedAtUtc);

            await ExecuteStatusCode(
                await controller.Refresh(new RefreshTokenRequestModel
                {
                    UserId = login.UserId,
                    RefreshToken = login.RefreshToken,
                    RefreshSessionId = login.RefreshSessionId
                }),
                serviceProvider,
                StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task Login_Should_Use_Configured_Token_Lifetimes()
        {
            await using var serviceProvider = BuildServiceProvider();
            var userManager = serviceProvider.GetRequiredService<UserManager<UserIdentity>>();
            var dbContext = serviceProvider.GetRequiredService<UserDbContext>();
            var controller = new AuthController(
                BuildConfiguration(accessTokenLifetimeMinutes: 5, refreshTokenLifetimeDays: 2),
                userManager,
                dbContext);
            const string email = "configured-lifetime@example.com";
            const string password = "123";

            var createUserResult = await userManager.CreateAsync(new UserIdentity
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            }, password);
            Assert.True(createUserResult.Succeeded);

            var beforeLogin = DateTime.UtcNow;
            var login = await ExecuteOk<LoginResponseModel>(
                await controller.Login(new LoginRequestModel { UserName = email, Password = password }),
                serviceProvider);
            var afterLogin = DateTime.UtcNow;

            Assert.InRange(
                login.ExpiresAtUtc,
                beforeLogin.AddMinutes(5).AddSeconds(-5),
                afterLogin.AddMinutes(5).AddSeconds(5));
            Assert.InRange(
                login.RefreshTokenExpiresAtUtc,
                beforeLogin.AddDays(2).AddSeconds(-5),
                afterLogin.AddDays(2).AddSeconds(5));
        }

        private static ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<UserDbContext>(options =>
                options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services
                .AddIdentityCore<UserIdentity>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 3;
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>();

            return services.BuildServiceProvider();
        }

        private static IConfiguration BuildConfiguration(
            int? accessTokenLifetimeMinutes = null,
            int? refreshTokenLifetimeDays = null)
        {
            var values = new Dictionary<string, string?>
                {
                    ["Jwt:Issuer"] = "LaptopStoreTests",
                    ["Jwt:Audience"] = "LaptopStoreTests",
                    ["Jwt:Key"] = "LaptopStoreTests_Jwt_Key_With_Enough_Length_For_HmacSha512_2026_extra"
                };

            if (accessTokenLifetimeMinutes.HasValue)
            {
                values["RefreshTokens:AccessTokenLifetimeMinutes"] = accessTokenLifetimeMinutes.Value.ToString();
            }

            if (refreshTokenLifetimeDays.HasValue)
            {
                values["RefreshTokens:RefreshTokenLifetimeDays"] = refreshTokenLifetimeDays.Value.ToString();
            }

            return new ConfigurationBuilder()
                .AddInMemoryCollection(values)
                .Build();
        }

        private static async Task<T> ExecuteOk<T>(IResult result, IServiceProvider serviceProvider)
        {
            var context = new DefaultHttpContext
            {
                RequestServices = serviceProvider
            };
            context.Response.Body = new MemoryStream();

            await result.ExecuteAsync(context);

            Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);
            context.Response.Body.Position = 0;
            var value = await JsonSerializer.DeserializeAsync<T>(
                context.Response.Body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.NotNull(value);
            return value;
        }

        private static async Task ExecuteStatusCode(IResult result, IServiceProvider serviceProvider, int expectedStatusCode)
        {
            var context = new DefaultHttpContext
            {
                RequestServices = serviceProvider
            };
            context.Response.Body = new MemoryStream();

            await result.ExecuteAsync(context);

            Assert.Equal(expectedStatusCode, context.Response.StatusCode);
        }
    }
}

using ShareView.Constants;
using ShareView.DTO;

namespace CustomerSite.Authentication;

public static class AuthCookieHelper
{
    public static void AppendAuthCookies(HttpContext context, LoginResponseModel loginResponse)
    {
        var accessTokenCookieOption = BuildCookieOptions(context, loginResponse.ExpiresAtUtc);
        var refreshTokenCookieOption = BuildCookieOptions(context, loginResponse.RefreshTokenExpiresAtUtc);

        context.Response.Cookies.Append(Variable.JWT_Token, loginResponse.Value, accessTokenCookieOption);
        context.Response.Cookies.Append(Variable.Refresh_Token, loginResponse.RefreshToken, refreshTokenCookieOption);
        context.Response.Cookies.Append(Variable.Refresh_UserId, loginResponse.UserId, refreshTokenCookieOption);
    }

    public static void DeleteAuthCookies(HttpContext context)
    {
        context.Response.Cookies.Delete(Variable.JWT_Token);
        context.Response.Cookies.Delete(Variable.Refresh_Token);
        context.Response.Cookies.Delete(Variable.Refresh_UserId);
    }

    private static CookieOptions BuildCookieOptions(HttpContext context, DateTime expiresAtUtc)
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Expires = expiresAtUtc,
            IsEssential = true,
            SameSite = SameSiteMode.Lax,
            Secure = context.Request.IsHttps
        };
    }
}

using CustomerSite.Authentication;
using Newtonsoft.Json;
using ShareView.Constants;
using ShareView.DTO;
using System.Text;

namespace CustomerSite.Middleware;

public class RefreshAccessTokenMiddleware
{
    private readonly RequestDelegate _next;

    public RefreshAccessTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IHttpClientFactory httpClientFactory)
    {
        if (ShouldTryRefresh(context))
        {
            var refreshed = await TryRefreshAccessToken(context, httpClientFactory);
            if (refreshed && HttpMethods.IsGet(context.Request.Method))
            {
                var redirectUrl = context.Request.PathBase + context.Request.Path + context.Request.QueryString;
                context.Response.Redirect(redirectUrl);
                return;
            }
        }

        await _next(context);
    }

    private static bool ShouldTryRefresh(HttpContext context)
    {
        if (!string.IsNullOrEmpty(context.Request.Cookies[Variable.JWT_Token]))
        {
            return false;
        }

        if (string.IsNullOrEmpty(context.Request.Cookies[Variable.Refresh_Token]) ||
            string.IsNullOrEmpty(context.Request.Cookies[Variable.Refresh_UserId]) ||
            string.IsNullOrEmpty(context.Request.Cookies[Variable.Refresh_SessionId]))
        {
            return false;
        }

        var path = context.Request.Path;
        return !path.StartsWithSegments("/Account/Login") &&
               !path.StartsWithSegments("/Account/Register") &&
               !path.StartsWithSegments("/Account/Logout");
    }

    private static async Task<bool> TryRefreshAccessToken(HttpContext context, IHttpClientFactory httpClientFactory)
    {
        var refreshToken = context.Request.Cookies[Variable.Refresh_Token];
        var userId = context.Request.Cookies[Variable.Refresh_UserId];
        var refreshSessionId = context.Request.Cookies[Variable.Refresh_SessionId];

        var request = new RefreshTokenRequestModel
        {
            UserId = userId ?? string.Empty,
            RefreshToken = refreshToken ?? string.Empty,
            RefreshSessionId = refreshSessionId ?? string.Empty
        };

        try
        {
            var httpClient = httpClientFactory.CreateClient();
            var requestJson = JsonConvert.SerializeObject(request);
            var response = await httpClient.PostAsync(
                "/Auth/refresh",
                new StringContent(requestJson, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                AuthCookieHelper.DeleteAuthCookies(context);
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(content);
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Value))
            {
                AuthCookieHelper.DeleteAuthCookies(context);
                return false;
            }

            AuthCookieHelper.AppendAuthCookies(context, loginResponse);
            return true;
        }
        catch
        {
            AuthCookieHelper.DeleteAuthCookies(context);
            return false;
        }
    }
}

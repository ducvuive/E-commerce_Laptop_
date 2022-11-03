﻿using ShareView.Constants;
using System.Net.Http.Headers;

namespace CustomerSite.Clients
{
    public class BaseClient
    {
        protected readonly HttpClient httpClient;

        public BaseClient(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            httpClient = clientFactory.CreateClient();

            //var token = httpContextAccessor.HttpContext?.Session.GetString(Variable.JWT);
            var token = httpContextAccessor.HttpContext?.Request.Cookies[Variable.JWT_Token];

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
        public BaseClient(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient();
        }
    }
}

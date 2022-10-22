namespace CustomerSite.Clients
{
    public class BaseClient
    {
        protected readonly HttpClient httpClient;
        public BaseClient(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient();
        }
    }
}

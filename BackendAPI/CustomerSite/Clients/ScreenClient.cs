using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IScreenClient
    {
        Task<ScreenDTO> GetScreen(int Id);
    }
    public class ScreenClient : BaseClient, IScreenClient
    {

        public ScreenClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<ScreenDTO> GetScreen(int Id)
        {
            var response = await httpClient.GetAsync("api/Screen/" + Id);
            var contents = await response.Content.ReadAsStringAsync();
            var screen = JsonConvert.DeserializeObject<ScreenDTO>(contents);
            return screen ?? new ScreenDTO();
        }
    }
}

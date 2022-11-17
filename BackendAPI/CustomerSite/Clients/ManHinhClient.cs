using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IManHinhClient
    {
        Task<ScreenDTO> GetManHinh(int Id);
    }
    public class ManHinhClient : BaseClient, IManHinhClient
    {

        public ManHinhClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<ScreenDTO> GetManHinh(int Id)
        {
            var response = await httpClient.GetAsync("api/Screen/" + Id);
            var contents = await response.Content.ReadAsStringAsync();
            var manhinh = JsonConvert.DeserializeObject<ScreenDTO>(contents);
            return manhinh ?? new ScreenDTO();
        }
    }
}

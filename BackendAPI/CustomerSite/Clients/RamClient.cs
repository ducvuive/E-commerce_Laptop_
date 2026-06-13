using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IRamClient
    {
        Task<RamDTO> GetRam(int Id);
    }
    public class RamClient : BaseClient, IRamClient
    {

        public RamClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<RamDTO> GetRam(int Id)
        {
            var response = await httpClient.GetAsync("api/Ram/" + Id);
            var contents = await response.Content.ReadAsStringAsync();
            var screen = JsonConvert.DeserializeObject<RamDTO>(contents);
            return screen ?? new RamDTO();
        }
    }
}

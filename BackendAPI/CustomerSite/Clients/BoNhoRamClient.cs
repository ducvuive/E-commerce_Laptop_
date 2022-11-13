using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IBoNhoRamClient
    {
        Task<RamDTO> GetBoNhoRam(int Id);
    }
    public class BoNhoRamClient : BaseClient, IBoNhoRamClient
    {

        public BoNhoRamClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<RamDTO> GetBoNhoRam(int Id)
        {
            var response = await httpClient.GetAsync("api/Ram/" + Id);
            var contents = await response.Content.ReadAsStringAsync();
            var manhinh = JsonConvert.DeserializeObject<RamDTO>(contents);
            return manhinh ?? new RamDTO();
        }
    }
}

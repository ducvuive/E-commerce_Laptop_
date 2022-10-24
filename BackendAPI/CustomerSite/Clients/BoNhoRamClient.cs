using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IBoNhoRamClient
    {
        Task<BoNhoRamDTO> GetBoNhoRam(int Id);
    }
    public class BoNhoRamClient : BaseClient, IBoNhoRamClient
    {

        public BoNhoRamClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<BoNhoRamDTO> GetBoNhoRam(int Id)
        {
            var response = await httpClient.GetAsync("api/BoNhoRams/" + Id);
            var contents = await response.Content.ReadAsStringAsync();
            var manhinh = JsonConvert.DeserializeObject<BoNhoRamDTO>(contents);
            return manhinh ?? new BoNhoRamDTO();
        }
    }
}

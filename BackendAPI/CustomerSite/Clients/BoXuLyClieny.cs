using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IBoXuLyClient
    {
        Task<BoXuLyDTO> GetBoXuLy(int Id);
    }
    public class BoXuLyClieny : BaseClient, IBoXuLyClient
    {

        public BoXuLyClieny(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<BoXuLyDTO> GetBoXuLy(int Id)
        {
            var response = await httpClient.GetAsync("api/BoXuLies/" + Id);
            var contents = await response.Content.ReadAsStringAsync();
            var manhinh = JsonConvert.DeserializeObject<BoXuLyDTO>(contents);
            return manhinh ?? new BoXuLyDTO();
        }
    }
}

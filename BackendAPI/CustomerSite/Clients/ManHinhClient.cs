using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IManHinhClient
    {
        Task<ManHinhDTO> GetManHinh(int Id);
    }
    public class ManHinhClient : BaseClient, IManHinhClient
    {

        public ManHinhClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<ManHinhDTO> GetManHinh(int Id)
        {
            var response = await httpClient.GetAsync("api/ManHinhs/" + Id);
            var contents = await response.Content.ReadAsStringAsync();
            var manhinh = JsonConvert.DeserializeObject<ManHinhDTO>(contents);
            return manhinh ?? new ManHinhDTO();
        }
    }
}

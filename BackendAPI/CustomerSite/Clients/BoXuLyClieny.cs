using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IBoXuLyClient
    {
        Task<ProcessorDTO> GetBoXuLy(int Id);
    }
    public class BoXuLyClieny : BaseClient, IBoXuLyClient
    {

        public BoXuLyClieny(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<ProcessorDTO> GetBoXuLy(int Id)
        {
            var response = await httpClient.GetAsync("api/Processor/" + Id);
            var contents = await response.Content.ReadAsStringAsync();
            var manhinh = JsonConvert.DeserializeObject<ProcessorDTO>(contents);
            return manhinh ?? new ProcessorDTO();
        }
    }
}

using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IProcessorClient
    {
        Task<ProcessorDTO> GetProcessor(int Id);
    }
    public class ProcessorClient : BaseClient, IProcessorClient
    {

        public ProcessorClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<ProcessorDTO> GetProcessor(int Id)
        {
            var response = await httpClient.GetAsync("api/Processor/" + Id);
            var contents = await response.Content.ReadAsStringAsync();
            var screen = JsonConvert.DeserializeObject<ProcessorDTO>(contents);
            return screen ?? new ProcessorDTO();
        }
    }
}

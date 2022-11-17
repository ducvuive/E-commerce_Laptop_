using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IDMClient
    {
        Task<List<CategoryDTO>> GetDMSP();
    }
    public class DanhMucClient : BaseClient, IDMClient
    {

        public DanhMucClient(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor) : base(clientFactory, httpContextAccessor)
        {
        }

        public async Task<List<CategoryDTO>> GetDMSP()
        {

            var response = await httpClient.GetAsync("api/Categories/GetCate");
            var contents = await response.Content.ReadAsStringAsync();
            var dmsp = JsonConvert.DeserializeObject<List<CategoryDTO>>(contents);
            return dmsp ?? new List<CategoryDTO>();
        }
    }
}

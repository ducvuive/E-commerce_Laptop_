using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IDMClient
    {
        Task<List<DanhMucSanPhamDTO>> GetDMSP();
    }
    public class DanhMucClient : BaseClient, IDMClient
    {

        public DanhMucClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<List<DanhMucSanPhamDTO>> GetDMSP()
        {
            var response = await httpClient.GetAsync("api/DanhMucSanPhams");
            var contents = await response.Content.ReadAsStringAsync();
            var dmsp = JsonConvert.DeserializeObject<List<DanhMucSanPhamDTO>>(contents);
            return dmsp ?? new List<DanhMucSanPhamDTO>();
        }
    }
}

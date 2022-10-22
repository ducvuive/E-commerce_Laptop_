using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IProductClient
    {
        Task<List<SanPhamDTO>> GetAllProduct();
    }
    public class ProductClient : BaseClient, IProductClient
    {

        public ProductClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<List<SanPhamDTO>> GetAllProduct()
        {
            var response = await httpClient.GetAsync("api/SanPhams/all");
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<SanPhamDTO>>(contents);
            return products ?? new List<SanPhamDTO>();
        }
        //}
    }
}

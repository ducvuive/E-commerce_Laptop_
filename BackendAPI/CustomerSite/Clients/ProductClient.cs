using Newtonsoft.Json;
using ShareView.DTO;
using System.Text;

namespace CustomerSite.Clients
{
    public interface IProductClient
    {
        Task<List<ProductDTO>> GetAllProducts();
        Task<int> GetPage();
        Task<List<ProductDTO>> GetProductsByPage(int page);
        Task<List<ProductDTO>> GetNewestProducts();
        Task<ProductDTO> GetProduct(int Id);
        Task<List<ProductDTO>> GetProductsByCategory(int categoryId);
        Task<List<ProductDTO>> GetProductsByCategoryPage(int categoryId, int page);
        Task<List<ProductDTO>> GetProductsByNamePage(string name, int page);
        Task<List<ProductDTO>> GetProductsByName(string name);
        Task CreateRating(RatingDTO rating, string userName);
    }
    public class ProductClient : BaseClient, IProductClient
    {
        public ProductClient(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor) : base(clientFactory, httpContextAccessor)
        {
        }
        public ProductClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<int> GetPage()
        {
            var response = await httpClient.GetAsync("api/Product/all");
            var contents = await response.Content.ReadAsStringAsync();
            //float temp = contents.Count() / (float)12;
            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            var sp_length = products.Count();
            int totalPage = (int)(sp_length / (float)12) + 1;
            return totalPage;
        }
        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var response = await httpClient.GetAsync("api/Product/all");
            var contents = await response.Content.ReadAsStringAsync();
            //float temp = contents.Count() / (float)12;
            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>(); ;
        }
        public async Task<List<ProductDTO>> GetNewestProducts()
        {
            var response = await httpClient.GetAsync("api/Product/month");
            var contents = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>();
        }
        public async Task<List<ProductDTO>> GetProductsByPage(int page)
        {
            var response = await httpClient.GetAsync("api/Product/GetProductsByPage/" + page);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>();
        }

        public async Task<List<ProductDTO>> GetProductsByCategoryPage(int categoryId, int page)
        {
            var response = await httpClient.GetAsync("api/Product/GetProductsByCategoryPage/" + categoryId + "/" + page);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>();
        }

        public async Task<List<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            var response = await httpClient.GetAsync("api/Product/GetProductsByCategory/" + categoryId);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>();
        }

        public async Task<List<ProductDTO>> GetProductsByNamePage(string name, int page)
        {
            var response = await httpClient.GetAsync("api/Product/GetProductsByNamePage/" + name + "/" + page);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>();
        }

        public async Task<List<ProductDTO>> GetProductsByName(string name)
        {
            var response = await httpClient.GetAsync("api/Product/GetProductsByName/" + name);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>();
        }
        public async Task<ProductDTO> GetProduct(int Id)
        {
            var response = await httpClient.GetAsync("api/Product/" + Id);
            var contents = await response.Content.ReadAsStringAsync();

            var product = JsonConvert.DeserializeObject<ProductDTO>(contents);
            return product ?? new ProductDTO();
        }
        public async Task CreateRating(RatingDTO rating, string userName)
        {
            var rating_ = JsonConvert.SerializeObject(rating);
            var response = await httpClient.PostAsync("api/Rating/" + userName, new StringContent(rating_, Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();
        }
    }
}

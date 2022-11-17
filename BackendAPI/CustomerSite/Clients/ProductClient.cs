using Newtonsoft.Json;
using ShareView.DTO;
using System.Text;

namespace CustomerSite.Clients
{
    public interface IProductClient
    {
        Task<List<ProductDTO>> GetTatCaSanPham();
        Task<int> GetPage();
        Task<List<ProductDTO>> GetSanPhamTheoTrang(int page);
        Task<List<ProductDTO>> GetSanPhamTopRaMat();
        Task<ProductDTO> GetSanPham(int Id);
        Task<List<ProductDTO>> GetSanPhamTheoDM(int dm);
        Task<List<ProductDTO>> GetSanPhamTheoDmTheoTrang(int dm, int page);
        Task<List<ProductDTO>> GetSanPhamTheoTenTheoTrang(string ten, int page);
        Task<List<ProductDTO>> GetSanPhamTheoTen(string ten);
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
        public async Task<List<ProductDTO>> GetTatCaSanPham()
        {
            var response = await httpClient.GetAsync("api/Product/all");
            var contents = await response.Content.ReadAsStringAsync();
            //float temp = contents.Count() / (float)12;
            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>(); ;
        }
        public async Task<List<ProductDTO>> GetSanPhamTopRaMat()
        {
            var response = await httpClient.GetAsync("api/Product/month");
            var contents = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>();
        }
        public async Task<List<ProductDTO>> GetSanPhamTheoTrang(int page)
        {
            var response = await httpClient.GetAsync("api/Product/GetSanPhamTheoTrang/" + page);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>();
        }

        public async Task<List<ProductDTO>> GetSanPhamTheoDmTheoTrang(int dm, int page)
        {
            var response = await httpClient.GetAsync("api/Product/GetSanPhamTheoDmTheoTrang/" + dm + "/" + page);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>();
        }

        public async Task<List<ProductDTO>> GetSanPhamTheoDM(int dm)
        {
            var response = await httpClient.GetAsync("api/Product/GetSanPhamTheoDM/" + dm);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>();
        }

        public async Task<List<ProductDTO>> GetSanPhamTheoTenTheoTrang(string ten, int page)
        {
            var response = await httpClient.GetAsync("api/Product/GetSanPhamTheoTenTheoTrang/" + ten + "/" + page);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>();
        }

        public async Task<List<ProductDTO>> GetSanPhamTheoTen(string ten)
        {
            var response = await httpClient.GetAsync("api/Product/GetSanPhamTheoTen/" + ten);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(contents);
            return products ?? new List<ProductDTO>();
        }
        public async Task<ProductDTO> GetSanPham(int Id)
        {
            var response = await httpClient.GetAsync("api/Product/" + Id);
            var contents = await response.Content.ReadAsStringAsync();

            var sanpham = JsonConvert.DeserializeObject<ProductDTO>(contents);
            return sanpham ?? new ProductDTO();
        }
        public async Task CreateRating(RatingDTO rating, string userName)
        {
            var rating_ = JsonConvert.SerializeObject(rating);
            var response = await httpClient.PostAsync("api/Rating/" + userName, new StringContent(rating_, Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();
        }
    }
}

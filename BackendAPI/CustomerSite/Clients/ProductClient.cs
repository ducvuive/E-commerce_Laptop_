using Newtonsoft.Json;
using ShareView.DTO;
using System.Text;

namespace CustomerSite.Clients
{
    public interface IProductClient
    {
        Task<List<SanPhamDTO>> GetTatCaSanPham();
        Task<int> GetPage();
        Task<List<SanPhamDTO>> GetSanPhamTheoTrang(int page);
        Task<List<SanPhamDTO>> GetSanPhamTopRaMat();
        Task<SanPhamDTO> GetSanPham(int Id);
        Task<List<SanPhamDTO>> GetSanPhamTheoDM(int dm);
        Task<List<SanPhamDTO>> GetSanPhamTheoDmTheoTrang(int dm, int page);
        Task<List<SanPhamDTO>> GetSanPhamTheoTenTheoTrang(string ten, int page);
        Task<List<SanPhamDTO>> GetSanPhamTheoTen(string ten);
        Task<RatingDTO> CreateRating(RatingDTO rating, string userName);
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
            var response = await httpClient.GetAsync("api/SanPhams/all");
            var contents = await response.Content.ReadAsStringAsync();
            //float temp = contents.Count() / (float)12;
            var products = JsonConvert.DeserializeObject<List<SanPhamDTO>>(contents);
            var sp_length = products.Count();
            int totalPage = (int)(sp_length / (float)12) + 1;
            return totalPage;
        }
        public async Task<List<SanPhamDTO>> GetTatCaSanPham()
        {
            var response = await httpClient.GetAsync("api/SanPhams/all");
            var contents = await response.Content.ReadAsStringAsync();
            //float temp = contents.Count() / (float)12;
            var products = JsonConvert.DeserializeObject<List<SanPhamDTO>>(contents);
            return products ?? new List<SanPhamDTO>(); ;
        }
        public async Task<List<SanPhamDTO>> GetSanPhamTopRaMat()
        {
            var response = await httpClient.GetAsync("api/SanPhams/month");
            var contents = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<SanPhamDTO>>(contents);
            return products ?? new List<SanPhamDTO>();
        }
        public async Task<List<SanPhamDTO>> GetSanPhamTheoTrang(int page)
        {
            var response = await httpClient.GetAsync("api/SanPhams/GetSanPhamTheoTrang/" + page);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<SanPhamDTO>>(contents);
            return products ?? new List<SanPhamDTO>();
        }

        public async Task<List<SanPhamDTO>> GetSanPhamTheoDmTheoTrang(int dm, int page)
        {
            var response = await httpClient.GetAsync("api/SanPhams/GetSanPhamTheoDmTheoTrang/" + dm + "/" + page);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<SanPhamDTO>>(contents);
            return products ?? new List<SanPhamDTO>();
        }

        public async Task<List<SanPhamDTO>> GetSanPhamTheoDM(int dm)
        {
            var response = await httpClient.GetAsync("api/SanPhams/GetSanPhamTheoDM/" + dm);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<SanPhamDTO>>(contents);
            return products ?? new List<SanPhamDTO>();
        }

        public async Task<List<SanPhamDTO>> GetSanPhamTheoTenTheoTrang(string ten, int page)
        {
            var response = await httpClient.GetAsync("api/SanPhams/GetSanPhamTheoTenTheoTrang/" + ten + "/" + page);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<SanPhamDTO>>(contents);
            return products ?? new List<SanPhamDTO>();
        }

        public async Task<List<SanPhamDTO>> GetSanPhamTheoTen(string ten)
        {
            var response = await httpClient.GetAsync("api/SanPhams/GetSanPhamTheoTen/" + ten);
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<SanPhamDTO>>(contents);
            return products ?? new List<SanPhamDTO>();
        }
        public async Task<SanPhamDTO> GetSanPham(int Id)
        {
            var response = await httpClient.GetAsync("api/SanPhams/" + Id);
            var contents = await response.Content.ReadAsStringAsync();

            var sanpham = JsonConvert.DeserializeObject<SanPhamDTO>(contents);
            return sanpham ?? new SanPhamDTO();
        }
        public async Task<RatingDTO> CreateRating(RatingDTO rating, string userName)
        {
            var rating_ = JsonConvert.SerializeObject(rating);
            var response = await httpClient.PostAsync("api/Rating/" + userName, new StringContent(rating_, Encoding.UTF8, "application/json"));
            /*var response = await httpClient.PostAsync("api/Rating/" + rating + userName);*/
            var contents = await response.Content.ReadAsStringAsync();

            var sanpham = JsonConvert.DeserializeObject<RatingDTO>(contents);
            return sanpham ?? new RatingDTO();
        }
    }
}

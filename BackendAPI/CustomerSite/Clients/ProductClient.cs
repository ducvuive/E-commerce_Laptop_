﻿using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IProductClient
    {
        Task<int> GetPage();
        Task<List<SanPhamDTO>> GetSanPhamTheoTrang(int page);
        Task<List<SanPhamDTO>> GetSanPhamTopRaMat();
        Task<SanPhamDTO> GetSanPham(int Id);
    }
    public class ProductClient : BaseClient, IProductClient
    {

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
        public async Task<SanPhamDTO> GetSanPham(int Id)
        {
            var response = await httpClient.GetAsync("api/SanPhams/" + Id);
            var contents = await response.Content.ReadAsStringAsync();

            var sanpham = JsonConvert.DeserializeObject<SanPhamDTO>(contents);
            return sanpham ?? new SanPhamDTO();
        }
    }
}

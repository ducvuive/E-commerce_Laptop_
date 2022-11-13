﻿using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IDMClient
    {
        Task<List<DanhMucSanPhamDTO>> GetDMSP();
    }
    public class DanhMucClient : BaseClient, IDMClient
    {

        public DanhMucClient(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor) : base(clientFactory, httpContextAccessor)
        {
        }

        public async Task<List<DanhMucSanPhamDTO>> GetDMSP()
        {

            var response = await httpClient.GetAsync("api/Categories/GetCate");
            var contents = await response.Content.ReadAsStringAsync();
            var dmsp = JsonConvert.DeserializeObject<List<DanhMucSanPhamDTO>>(contents);
            return dmsp ?? new List<DanhMucSanPhamDTO>();
        }
    }
}

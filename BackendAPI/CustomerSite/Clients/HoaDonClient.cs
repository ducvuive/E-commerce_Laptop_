using Newtonsoft.Json;
using ShareView.DTO;
using System.Text;

namespace CustomerSite.Clients
{
    public interface IHoaDonClient
    {
        Task<HoaDonDTO> GetHoaDon();
        Task AddHoaDon(HoaDonDTO hoaDonDTO, string userName);
        Task AddCTHD(CTHD_DTO cthd_DTO);
    }
    public class HoaDonClient : BaseClient, IHoaDonClient
    {

        public HoaDonClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<HoaDonDTO> GetHoaDon()
        {
            var response = await httpClient.GetAsync("api/Invoice/");
            var contents = await response.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<HoaDonDTO>(contents);
            return id;
        }
        public async Task AddHoaDon(HoaDonDTO hoaDonDTO, string userName)
        {
            var hoaDon_ = JsonConvert.SerializeObject(hoaDonDTO);
            await httpClient.PostAsync("api/Invoice/" + userName, new StringContent(hoaDon_, Encoding.UTF8, "application/json"));
        }

        public async Task AddCTHD(CTHD_DTO cthd_DTO)
        {
            var cthd_ = JsonConvert.SerializeObject(cthd_DTO);
            await httpClient.PostAsync("api/InvoiceDetail/", new StringContent(cthd_, Encoding.UTF8, "application/json"));
        }
    }
}

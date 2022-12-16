using Newtonsoft.Json;
using ShareView.DTO;
using System.Text;

namespace CustomerSite.Clients
{
    public interface IHoaDonClient
    {
        Task<InvoiceDTO> GetHoaDon();
        Task AddHoaDon(InvoiceDTO hoaDonDTO, string userName);
        Task AddCTHD(InvoiceDetailDTO cthd_DTO);
    }
    public class HoaDonClient : BaseClient, IHoaDonClient
    {

        public HoaDonClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<InvoiceDTO> GetHoaDon()
        {
            var response = await httpClient.GetAsync("/api/Invoice/");
            var contents = await response.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<InvoiceDTO>(contents);
            return id;
        }
        public async Task AddHoaDon(InvoiceDTO hoaDonDTO, string email)
        {
            var hoaDon_ = JsonConvert.SerializeObject(hoaDonDTO);
            var a = await httpClient.PostAsync("/api/Invoice/" + email, new StringContent(hoaDon_, Encoding.UTF8, "application/json"));
            var b = 1;
        }

        public async Task AddCTHD(InvoiceDetailDTO cthd_DTO)
        {
            var cthd_ = JsonConvert.SerializeObject(cthd_DTO);
            var a = await httpClient.PostAsync("/api/InvoiceDetail", new StringContent(cthd_, Encoding.UTF8, "application/json"));
        }
    }
}

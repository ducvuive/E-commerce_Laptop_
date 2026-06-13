using Newtonsoft.Json;
using ShareView.DTO;
using System.Text;

namespace CustomerSite.Clients
{
    public interface IInvoiceClient
    {
        Task<InvoiceDTO> GetInvoice();
        Task AddInvoice(InvoiceDTO invoiceDTO, string userName);
        Task AddInvoiceDetail(InvoiceDetailDTO invoiceDetailDTO);
    }
    public class InvoiceClient : BaseClient, IInvoiceClient
    {

        public InvoiceClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<InvoiceDTO> GetInvoice()
        {
            var response = await httpClient.GetAsync("/api/Invoice/");
            var contents = await response.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<InvoiceDTO>(contents);
            return id;
        }
        public async Task AddInvoice(InvoiceDTO invoiceDTO, string email)
        {
            var invoice_ = JsonConvert.SerializeObject(invoiceDTO);
            var a = await httpClient.PostAsync("/api/Invoice/" + email, new StringContent(invoice_, Encoding.UTF8, "application/json"));
            var b = 1;
        }

        public async Task AddInvoiceDetail(InvoiceDetailDTO invoiceDetailDTO)
        {
            var invoiceDetailJson = JsonConvert.SerializeObject(invoiceDetailDTO);
            var a = await httpClient.PostAsync("/api/InvoiceDetail", new StringContent(invoiceDetailJson, Encoding.UTF8, "application/json"));
        }
    }
}

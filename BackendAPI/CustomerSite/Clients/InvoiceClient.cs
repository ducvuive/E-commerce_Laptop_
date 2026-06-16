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
        Task<CheckoutOrderResponseDTO> CheckoutOrder(CheckoutOrderRequestDTO checkoutOrderRequest);
    }
    public class InvoiceClient : BaseClient, IInvoiceClient
    {

        public InvoiceClient(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
            : base(clientFactory, httpContextAccessor)
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

        public async Task<CheckoutOrderResponseDTO> CheckoutOrder(CheckoutOrderRequestDTO checkoutOrderRequest)
        {
            var requestJson = JsonConvert.SerializeObject(checkoutOrderRequest);
            var response = await httpClient.PostAsync(
                "/api/Invoice/checkout",
                new StringContent(requestJson, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException(string.IsNullOrWhiteSpace(error)
                    ? "Checkout failed."
                    : error);
            }

            var contents = await response.Content.ReadAsStringAsync();
            var checkoutResponse = JsonConvert.DeserializeObject<CheckoutOrderResponseDTO>(contents);
            return checkoutResponse ?? throw new InvalidOperationException("Checkout response is invalid.");
        }
    }
}

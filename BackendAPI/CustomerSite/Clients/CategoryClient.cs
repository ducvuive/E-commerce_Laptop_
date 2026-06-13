using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface ICategoryClient
    {
        Task<List<CategoryDTO>> GetCategories();
    }
    public class CategoryClient : BaseClient, ICategoryClient
    {

        public CategoryClient(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor) : base(clientFactory, httpContextAccessor)
        {
        }

        public async Task<List<CategoryDTO>> GetCategories()
        {

            var response = await httpClient.GetAsync("api/Categories/GetCate");
            var contents = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(contents);
            return categories ?? new List<CategoryDTO>();
        }
    }
}

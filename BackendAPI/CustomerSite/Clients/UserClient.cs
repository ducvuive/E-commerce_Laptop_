using Newtonsoft.Json;
using ShareView.DTO;

namespace CustomerSite.Clients
{
    public interface IUserClient
    {
        Task<UserIdentityDTO> GetUser(string Id);
    }
    public class UserClient : BaseClient, IUserClient
    {

        public UserClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<UserIdentityDTO> GetUser(string email)
        {
            var response = await httpClient.GetAsync("api/User/" + email);
            var contents = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserIdentityDTO>(contents);
            return user ?? new UserIdentityDTO();
        }
    }
}

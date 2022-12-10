using InnoGotchiGameFrontEnd.DAL.Models.Users;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class UserService : BaseService
    {
        private string _clientName;

        public UserService(IHttpClientFactory httpClientFactory, string? authorize) : base(httpClientFactory, authorize)
        {
            _clientName = "Users";
        }

        public async Task<IEnumerable<User>> GetUsers(UserSorter? sorter = null, UserFiltrator? filtrator = null)
        {
            var httpClient = GetHttpClient(_clientName);
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            using StringContent jsonContent = new(
                         JsonSerializer.Serialize(new
                         {
                             sorter,
                             filtrator
                         }),
                         Encoding.UTF8,
                         "application/json");
            request.Content = jsonContent;
            var httpResponseMessage = await httpClient.SendAsync(request);

            IEnumerable<User> users = new List<User>();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                users = await JsonSerializer.DeserializeAsync<IEnumerable<User>>(contentStream, options);
            }
            return users;
        }

        public async Task<IEnumerable<User>> GetUsersPage(int pageSize, int pageNumber, UserSorter sorter, UserFiltrator filtrator)
        {
            var httpClient = GetHttpClient(_clientName);

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress + $"/{pageSize}/{pageNumber}");
            using StringContent jsonContent = new(
                         JsonSerializer.Serialize(new
                         {
                             sorter,
                             filtrator
                         }),
                         Encoding.UTF8,
                         "application/json");
            request.Content = jsonContent;
            var httpResponseMessage = await httpClient.SendAsync(request);

            IEnumerable<User> users = new List<User>();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                users = await JsonSerializer.DeserializeAsync<IEnumerable<User>>(contentStream, options);
            }
            return users;
        }
        public async Task<User?> GetUserById(int id)
        {
            var httpClient = GetHttpClient(_clientName);
            var httpResponseMessage = await httpClient.GetAsync(httpClient.BaseAddress + $"/{id}");

            User? user = null;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                user = await JsonSerializer.DeserializeAsync<User>(contentStream, options);
            }
            return user;
        }
        public async Task<User?> GetAuthodizedUser()
        {
            var httpClient = GetHttpClient(_clientName);
            var httpResponseMessage = await httpClient.GetAsync(httpClient.BaseAddress + $"/Authorized");

            User? user = null;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                user = await JsonSerializer.DeserializeAsync<User>(contentStream, options);
            }
            return user;
        }

        public async Task<ServiceRezult> Create(AddUserModel addModel)
        {
            var httpClient = GetHttpClient(_clientName);
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PostAsync("", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdateUserData(UpdateUserDataModel updateModel)
        {
            var httpClient = GetHttpClient(_clientName);

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PutAsync(httpClient.BaseAddress + "/data", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
        public async Task<ServiceRezult> UpdateUserPassword(UpdateUserPasswordModel updateModel)
        {
            var httpClient = GetHttpClient(_clientName);

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PutAsync(httpClient.BaseAddress + "/password", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
        public async Task<ServiceRezult> DeleteById(int userId)
        {
            var httpClient = GetHttpClient(_clientName);

            var httpResponseMessage = await httpClient.DeleteAsync(httpClient.BaseAddress + $"/{userId}");

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<string?> Authorize(string email, string password)
        {
            var httpClient = GetHttpClient(_clientName);
            var parameters = new Dictionary<string, string>();
            parameters["email"] = email;
            parameters["password"] = password;
            var httpResponseMessage = await httpClient.PostAsync(httpClient.BaseAddress + "/token", new FormUrlEncodedContent(parameters));

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var token = (await httpResponseMessage.Content.ReadAsStringAsync()).Replace("\"", "");
                AccessToken = token;
                return token;
            }
            return null;
        }
    }
}

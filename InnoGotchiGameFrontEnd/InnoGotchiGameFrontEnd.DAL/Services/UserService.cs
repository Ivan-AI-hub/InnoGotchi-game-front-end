using InnoGotchiGameFrontEnd.DAL.Models.Users;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class UserService : BaseService
    {
        private string _clientName;

        public UserService(HttpClient client) : base(client)
        {
            _clientName = "Users";
            RequestClient.BaseAddress = new Uri(RequestClient.BaseAddress, _clientName);
        }

        public async Task<IEnumerable<User>> GetUsers(UserSorter? sorter = null, UserFiltrator? filtrator = null)
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, RequestClient.BaseAddress);

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
            var httpResponseMessage = await RequestClient.SendAsync(request);

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
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, RequestClient.BaseAddress + $"/{pageSize}/{pageNumber}");
            using StringContent jsonContent = new(
                         JsonSerializer.Serialize(new
                         {
                             sorter,
                             filtrator
                         }),
                         Encoding.UTF8,
                         "application/json");
            request.Content = jsonContent;
            var httpResponseMessage = await RequestClient.SendAsync(request);

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
            var httpResponseMessage = await RequestClient.GetAsync(RequestClient.BaseAddress + $"/{id}");

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
            var httpResponseMessage = await RequestClient.GetAsync(RequestClient.BaseAddress + $"/Authorized");

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
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PostAsync("", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdateUserData(UpdateUserDataModel updateModel)
        {
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PutAsync(RequestClient.BaseAddress + "/data", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
        public async Task<ServiceRezult> UpdateUserPassword(UpdateUserPasswordModel updateModel)
        {
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PutAsync(RequestClient.BaseAddress + "/password", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
        public async Task<ServiceRezult> DeleteById(int userId)
        {
            var httpResponseMessage = await RequestClient.DeleteAsync(RequestClient.BaseAddress + $"/{userId}");

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<AuthorizeModel?> Authorize(string email, string password)
        {
            var parameters = new Dictionary<string, string>();
            parameters["email"] = email;
            parameters["password"] = password;
            var httpResponseMessage = await RequestClient.PostAsync(RequestClient.BaseAddress + "/token", new FormUrlEncodedContent(parameters));

            if (httpResponseMessage.IsSuccessStatusCode)
            {
				using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};
				var token = await JsonSerializer.DeserializeAsync<AuthorizeModel>(contentStream, options);
                return token;
            }
            return null;
        }
    }
}

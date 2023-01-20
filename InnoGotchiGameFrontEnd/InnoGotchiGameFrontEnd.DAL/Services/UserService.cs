using InnoGotchiGameFrontEnd.DAL.Models.Users;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class UserService : BaseService
    {
        private Uri _baseUri;

        public UserService(HttpClient client) : base(client)
        {
            var apiControllerName = "Users";
            _baseUri = new Uri(client.BaseAddress, apiControllerName);
        }

        public async Task<IEnumerable<User>> GetUsers(UserSorter? sorter = null, UserFiltrator? filtrator = null)
        {
            var requestUrl = new StringBuilder($"?sortField={sorter.SortRule}" +
                                               $"&isDescendingSort={sorter.IsDescendingSort}");

            if (!String.IsNullOrEmpty(filtrator.FirstName))
            {
                requestUrl.Append($"&firstName={filtrator.FirstName}");
            }
            if (!String.IsNullOrEmpty(filtrator.LastName))
            {
                requestUrl.Append($"&lastName={filtrator.LastName}");
            }
            if (!String.IsNullOrEmpty(filtrator.Email))
            {
                requestUrl.Append($"&email={filtrator.Email}");
            }
            if (!String.IsNullOrEmpty(filtrator.Email))
            {
                requestUrl.Append($"&petFarnId={filtrator.PetFarmId}");
            }
            var users = await RequestClient.GetFromJsonAsync<IEnumerable<User>>(_baseUri + requestUrl.ToString());

            if (users == null)
            {
                throw new Exception("BadRequest in UserService GetUsers");
            }
            return users;
        }

        public async Task<IEnumerable<User>> GetUsersPage(int pageSize, int pageNumber, UserSorter sorter, UserFiltrator filtrator)
        {
            var requestUrl = new StringBuilder($"/{pageSize}/{pageNumber}" +
                             $"?sortField={sorter.SortRule}&isDescendingSort={sorter.IsDescendingSort}");

            if (!String.IsNullOrEmpty(filtrator.FirstName))
            {
                requestUrl.Append($"&firstName={filtrator.FirstName}");
            }
            if (!String.IsNullOrEmpty(filtrator.LastName))
            {
                requestUrl.Append($"&lastName={filtrator.LastName}");
            }
            if (!String.IsNullOrEmpty(filtrator.Email))
            {
                requestUrl.Append($"&email={filtrator.Email}");
            }
            if (!String.IsNullOrEmpty(filtrator.Email))
            {
                requestUrl.Append($"&petFarnId={filtrator.PetFarmId}");
            }
            var users = await RequestClient.GetFromJsonAsync<IEnumerable<User>>(_baseUri + requestUrl.ToString());

            if (users == null)
            {
                throw new Exception("BadRequest in UserService GetUsersPage");
            }
            return users;
        }
        public async Task<User?> GetUserById(int id)
        {
            User? user = await RequestClient.GetFromJsonAsync<User>(_baseUri + $"/{id}");

            return user;
        }
        public async Task<User?> GetAuthodizedUser()
        {
            User? user = await RequestClient.GetFromJsonAsync<User>(_baseUri + $"/Authorized");

            return user;
        }

        public async Task<ServiceRezult> Create(AddUserModel addModel)
        {
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PostAsync(_baseUri, jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdateUserData(UpdateUserDataModel updateModel)
        {
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PutAsync(_baseUri + "/data", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
        public async Task<ServiceRezult> UpdateUserPassword(UpdateUserPasswordModel updateModel)
        {
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PutAsync(_baseUri + "/password", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
        public async Task<ServiceRezult> DeleteById(int userId)
        {
            var httpResponseMessage = await RequestClient.DeleteAsync(_baseUri + $"/{userId}");

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<AuthorizeModel?> Authorize(string email, string password)
        {
            var parameters = new Dictionary<string, string>();
            parameters["email"] = email;
            parameters["password"] = password;
            var httpResponseMessage = await RequestClient.PostAsync(_baseUri + "/token", new FormUrlEncodedContent(parameters));

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

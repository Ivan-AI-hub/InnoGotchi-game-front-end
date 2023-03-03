using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.Domain;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Sorters;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class UserService : BaseService, IUserService
    {
        private Uri _baseUri;

        public UserService(IAuthorizedClient client) : base(client)
        {
            var apiControllerName = "Users";
            _baseUri = new Uri(client.BaseAddress, apiControllerName);
        }

        public async Task<IEnumerable<User>> GetAsync(UserSorter? sorter = null, UserFiltrator? filtrator = null, CancellationToken cancellationToken = default)
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
            var users = await (await RequestClient).GetFromJsonAsync<IEnumerable<User>>(_baseUri + requestUrl.ToString(), cancellationToken);

            if (users == null)
            {
                throw new Exception("BadRequest in UserService GetUsers");
            }
            return users;
        }

        public async Task<IEnumerable<User>> GetPageAsync(int pageSize, int pageNumber, UserSorter sorter, UserFiltrator filtrator, CancellationToken cancellationToken = default)
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

            var users = await (await RequestClient).GetFromJsonAsync<IEnumerable<User>>(_baseUri + requestUrl.ToString(), cancellationToken);

            if (users == null)
            {
                throw new Exception("BadRequest in UserService GetUsersPage");
            }
            return users;
        }
        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            User? user = await (await RequestClient).GetFromJsonAsync<User>(_baseUri + $"/{id}", cancellationToken);

            return user;
        }
        public async Task<User?> GetAuthodizedUserAsync(CancellationToken cancellationToken = default)
        {
            User? user = await (await RequestClient).GetFromJsonAsync<User>(_baseUri + $"/Authorized", cancellationToken);

            return user;
        }

        public async Task<IServiceResult> CreateAsync(AddUserModel addModel, CancellationToken cancellationToken = default)
        {
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri, jsonContent, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }

        public async Task<IServiceResult> UpdateDataAsync(UpdateUserDataModel updateModel, CancellationToken cancellationToken = default)
        {
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + "/data", jsonContent, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }
        public async Task<IServiceResult> UpdatePasswordAsync(UpdateUserPasswordModel updateModel, CancellationToken cancellationToken = default)
        {
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + "/password", jsonContent, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }
        public async Task<IServiceResult> DeleteByIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            var httpResponseMessage = await (await RequestClient).DeleteAsync(_baseUri + $"/{userId}", cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }

        public async Task<AuthorizeModel?> AuthorizeAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();
            parameters["email"] = email;
            parameters["password"] = password;
            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri + "/token", new FormUrlEncodedContent(parameters), cancellationToken);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var token = await JsonSerializer.DeserializeAsync<AuthorizeModel>(contentStream, options, cancellationToken);
                return token;
            }
            return null;
        }
    }
}

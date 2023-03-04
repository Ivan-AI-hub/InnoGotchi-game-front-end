using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.DAL.UriConstructors;
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
            var query = UserUriConstructor.GenerateUriQuery(sorter, filtrator);
            var users = await (await RequestClient).GetFromJsonAsync<IEnumerable<User>>(_baseUri + query, cancellationToken);

            if (users == null)
            {
                throw new Exception("BadRequest in UserService GetUsers");
            }
            return users;
        }

        public async Task<IEnumerable<User>> GetPageAsync(int pageSize, int pageNumber, UserSorter sorter, UserFiltrator filtrator, CancellationToken cancellationToken = default)
        {
            var query = UserUriConstructor.GenerateUriQuery(sorter, filtrator);

            var users = await (await RequestClient).GetFromJsonAsync<IEnumerable<User>>(_baseUri + query, cancellationToken);

            if (users == null)
            {
                throw new Exception("BadRequest in UserService GetUsersPage");
            }
            return users;
        }
        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var requestUri = _baseUri + $"/{id}";
            User? user = await (await RequestClient).GetFromJsonAsync<User>(requestUri, cancellationToken);

            return user;
        }
        public async Task<User?> GetAuthodizedUserAsync(CancellationToken cancellationToken = default)
        {
            var requestUri = _baseUri + $"/Authorized";
            User? user = await (await RequestClient).GetFromJsonAsync<User>(requestUri, cancellationToken);

            return user;
        }

        public async Task<IServiceResult> CreateAsync(AddUserModel addModel, CancellationToken cancellationToken = default)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(addModel),Encoding.UTF8,"application/json");

            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri, jsonContent, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }

        public async Task<IServiceResult> UpdateDataAsync(UpdateUserDataModel updateModel, CancellationToken cancellationToken = default)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(updateModel),Encoding.UTF8,"application/json");
            var requestUri = _baseUri + $"/data";

            var httpResponseMessage = await (await RequestClient).PutAsync(requestUri, jsonContent, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }
        public async Task<IServiceResult> UpdatePasswordAsync(UpdateUserPasswordModel updateModel, CancellationToken cancellationToken = default)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(updateModel),Encoding.UTF8,"application/json");
            var requestUri = _baseUri + $"/password";

            var httpResponseMessage = await (await RequestClient).PutAsync(requestUri, jsonContent, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }
        public async Task<IServiceResult> DeleteByIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            var requestUri = _baseUri + $"/{userId}";
            var httpResponseMessage = await (await RequestClient).DeleteAsync(requestUri, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }

        public async Task<AuthorizeModel?> AuthorizeAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var quary = $"?{nameof(email)}={email}&{nameof(password)}={password}";
            var requestUri = _baseUri + $"/token" + quary;

            var authorizeModel = await (await RequestClient).GetFromJsonAsync<AuthorizeModel>(requestUri, cancellationToken);

            return authorizeModel;
        }
    }
}

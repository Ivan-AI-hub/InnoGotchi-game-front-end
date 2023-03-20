using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.DAL.UriConstructors;
using InnoGotchiGameFrontEnd.Domain;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Sorters;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class PetService : BaseService, IPetService
    {
        private Uri _baseUri;
        public PetService(IAuthorizedClient client) : base(client)
        {
            var apiControllerName = "pets";
            _baseUri = new Uri(client.BaseAddress, apiControllerName);
        }


        public async Task<IEnumerable<Pet>> GetAsync(PetSorter? sorter = null, PetFiltrator? filtrator = null, CancellationToken cancellationToken = default)
        {
            var quary = PetUriConstructor.GenerateUriQuery(sorter, filtrator);

            var pets = await (await RequestClient).GetFromJsonAsync<IEnumerable<Pet>>(_baseUri + quary, cancellationToken);

            if (pets == null)
            {
                throw new Exception("BadRequest in PetService GetPets");
            }
            return pets;
        }

        public async Task<IEnumerable<Pet>> GetPageAsync(int pageSize, int pageNumber, PetSorter sorter, PetFiltrator filtrator, CancellationToken cancellationToken = default)
        {
            var quary = PetUriConstructor.GenerateUriQuery(sorter, filtrator);
            var requestUri = _baseUri + $"/{pageSize}/{pageNumber}" + quary;

            var pets = await (await RequestClient).GetFromJsonAsync<IEnumerable<Pet>>(requestUri, cancellationToken);

            if (pets == null)
            {
                throw new Exception("BadRequest in PetService GetPetsPage");
            }
            return pets;
        }
        public async Task<Pet?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var requestUri = _baseUri + $"/{id}";
            Pet? Pet = await (await RequestClient).GetFromJsonAsync<Pet>(requestUri, cancellationToken);

            return Pet;
        }

        public async Task<IServiceResult> CreateAsync(AddPetModel addModel, CancellationToken cancellationToken = default)
        {

            using StringContent jsonContent = new(JsonSerializer.Serialize(addModel), Encoding.UTF8, "application/json");

            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri, jsonContent, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }

        public async Task<IServiceResult> UpdateAsync(UpdatePetModel updateModel, CancellationToken cancellationToken = default)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(updateModel), Encoding.UTF8, "application/json");
            var requestUri = _baseUri + $"/data";
            var httpResponseMessage = await (await RequestClient).PutAsync(requestUri, jsonContent, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }

        public async Task<IServiceResult> FeedAsync(int petId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();
            var requestUri = _baseUri + $"/{petId}/feed";
            var httpResponseMessage = await (await RequestClient).PutAsync(requestUri, new FormUrlEncodedContent(parameters), cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }

        public async Task<IServiceResult> GiveDrinkAsync(int petId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();
            var requestUri = _baseUri + $"/{petId}/drink";
            var httpResponseMessage = await (await RequestClient).PutAsync(requestUri, new FormUrlEncodedContent(parameters), cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }
        public async Task<IServiceResult> ResetHappinessDay(int petId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();
            var requestUri = _baseUri + $"/{petId}/resetHappinessDay";
            var httpResponseMessage = await (await RequestClient).PutAsync(requestUri, new FormUrlEncodedContent(parameters), cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }
        public async Task<IServiceResult> SetDeadStatus(int petId, DateTime deadDate, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(deadDate), deadDate.ToString() }
            };
            var requestUri = _baseUri + $"/{petId}/dead";
            var httpResponseMessage = await (await RequestClient).PutAsync(requestUri, new FormUrlEncodedContent(parameters), cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }
    }
}

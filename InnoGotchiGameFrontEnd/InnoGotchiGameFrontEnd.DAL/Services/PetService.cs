using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.DAL.Models.Pets;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class PetService : BaseService
    {
        private Uri _baseUri;
        public PetService(IAuthorizedClient client, CancellationToken cancellationToken = default) : base(client)
        {
            var apiControllerName = "pets";
            _baseUri = new Uri(client.BaseAddress, apiControllerName);
        }


        public async Task<IEnumerable<Pet>> GetAsync(PetSorter? sorter = null, PetFiltrator? filtrator = null, CancellationToken cancellationToken = default)
        {

            var requestUrl = new StringBuilder($"?sortField={sorter.SortRule}" +
                                   $"&isDescendingSort={sorter.IsDescendingSort}");

            if (!String.IsNullOrEmpty(filtrator.Name))
            {
                requestUrl.Append($"&Name={filtrator.Name}");
            }
            if (filtrator.DrinkingInterval != null)
            {
                requestUrl.Append($"&MaxDaysFromLastDrinking={filtrator.DrinkingInterval.MaxDays}" +
                                  $"&MinDaysFromLastDrinking={filtrator.DrinkingInterval.MinDays}");
            }
            if (filtrator.FeedingInterval != null)
            {
                requestUrl.Append($"&MaxDaysFromLastFeeding={filtrator.FeedingInterval.MaxDays}" +
                                  $"&MinDaysFromLastFeeding={filtrator.FeedingInterval.MinDays}");
            }

            requestUrl.Append($"&DaysAlive={filtrator.DaysAlive}");


            var pets = await (await RequestClient).GetFromJsonAsync<IEnumerable<Pet>>(_baseUri + requestUrl.ToString(), cancellationToken);

            if (pets == null)
            {
                throw new Exception("BadRequest in PetService GetPets");
            }
            return pets;
        }

        public async Task<IEnumerable<Pet>> GetPageAsync(int pageSize, int pageNumber, PetSorter sorter, PetFiltrator filtrator, CancellationToken cancellationToken = default)
        {

            var requestUrl = new StringBuilder($"/{pageSize}/{pageNumber}" +
                            $"?sortField={sorter.SortRule}&isDescendingSort={sorter.IsDescendingSort}");

            if (!String.IsNullOrEmpty(filtrator.Name))
            {
                requestUrl.Append($"&Name={filtrator.Name}");
            }
            if (filtrator.DrinkingInterval != null)
            {
                requestUrl.Append($"&MaxDaysFromLastDrinking={filtrator.DrinkingInterval.MaxDays}" +
                                  $"&MinDaysFromLastDrinking={filtrator.DrinkingInterval.MinDays}");
            }
            if (filtrator.FeedingInterval != null)
            {
                requestUrl.Append($"&MaxDaysFromLastFeeding={filtrator.FeedingInterval.MaxDays}" +
                                  $"&MinDaysFromLastFeeding={filtrator.FeedingInterval.MinDays}");
            }
            if (filtrator.DaysAlive != 0)
            {
                requestUrl.Append($"&DaysAlive={filtrator.DaysAlive}");
            }

            var pets = await (await RequestClient).GetFromJsonAsync<IEnumerable<Pet>>(_baseUri + requestUrl.ToString(), cancellationToken);

            if (pets == null)
            {
                throw new Exception("BadRequest in PetService GetPetsPage");
            }
            return pets;
        }
        public async Task<Pet?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            Pet? Pet = await (await RequestClient).GetFromJsonAsync<Pet>(_baseUri + $"/{id}", cancellationToken);

            return Pet;
        }

        public async Task<ServiceRezult> CreateAsync(AddPetModel addModel, CancellationToken cancellationToken = default)
        {

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri, jsonContent, cancellationToken);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdateAsync(UpdatePetModel updateModel, CancellationToken cancellationToken = default)
        {


            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + "/data", jsonContent, cancellationToken);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> FeedAsync(int petId, CancellationToken cancellationToken = default)
        {

            var parameters = new Dictionary<string, string>();

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + $"/{petId}/feed", new FormUrlEncodedContent(parameters), cancellationToken);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> GiveDrinkAsync(int petId, CancellationToken cancellationToken = default)
        {

            var parameters = new Dictionary<string, string>();

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + $"/{petId}/drink", new FormUrlEncodedContent(parameters), cancellationToken);

            return await GetCommandRezult(httpResponseMessage);
        }
        public async Task<ServiceRezult> ResetHappinessDay(int petId, CancellationToken cancellationToken = default)
        {

            var parameters = new Dictionary<string, string>();

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + $"/{petId}/resetHappinessDay", new FormUrlEncodedContent(parameters), cancellationToken);

            return await GetCommandRezult(httpResponseMessage);
        }
        public async Task<ServiceRezult> SetDeadStatus(int petId, DateTime deadDate, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(deadDate), deadDate.ToString() }
            };

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + $"/{petId}/dead", new FormUrlEncodedContent(parameters), cancellationToken);

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

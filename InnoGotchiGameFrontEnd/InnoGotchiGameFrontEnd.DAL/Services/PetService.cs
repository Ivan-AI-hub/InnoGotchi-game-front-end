using InnoGotchiGameFrontEnd.DAL.Models.Pets;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class PetService : BaseService
    {
        private Uri _baseUri;
        public PetService(HttpClient client) : base(client)
        {
            var apiControllerName = "pets";
            _baseUri = new Uri(client.BaseAddress, apiControllerName);
        }


        public async Task<IEnumerable<Pet>> GetPets(PetSorter? sorter = null, PetFiltrator? filtrator = null)
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


            var pets = await RequestClient.GetFromJsonAsync<IEnumerable<Pet>>(_baseUri + requestUrl.ToString());

            if (pets == null)
            {
                throw new Exception("BadRequest in PetService GetPets");
            }
            return pets;
        }

        public async Task<IEnumerable<Pet>> GetPetsPage(int pageSize, int pageNumber, PetSorter sorter, PetFiltrator filtrator)
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

            var pets = await RequestClient.GetFromJsonAsync<IEnumerable<Pet>>(_baseUri + requestUrl.ToString());

            if (pets == null)
            {
                throw new Exception("BadRequest in PetService GetPetsPage");
            }
            return pets;
        }
        public async Task<Pet?> GetPetById(int id)
        {
            Pet? Pet = await RequestClient.GetFromJsonAsync<Pet>(_baseUri + $"/{id}");

            return Pet;
        }

        public async Task<ServiceRezult> Create(AddPetModel addModel)
        {

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PostAsync(_baseUri, jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdatePet(UpdatePetModel updateModel)
        {


            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PutAsync(_baseUri + "/data", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> Feed(int petId)
        {

            var parameters = new Dictionary<string, string>();

            var httpResponseMessage = await RequestClient.PutAsync(_baseUri + $"/{petId}/feed", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> GiveDrink(int petId)
        {

            var parameters = new Dictionary<string, string>();

            var httpResponseMessage = await RequestClient.PutAsync(_baseUri + $"/{petId}/drink", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }
        public async Task<ServiceRezult> ResetHappinessDay(int petId)
        {

            var parameters = new Dictionary<string, string>();

            var httpResponseMessage = await RequestClient.PutAsync(_baseUri + $"/{petId}/resetHappinessDay", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }
        public async Task<ServiceRezult> SetDeadStatus(int petId, DateTime deadDate)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add(nameof(deadDate), deadDate.ToString());

            var httpResponseMessage = await RequestClient.PutAsync(_baseUri + $"/{petId}/dead", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

using InnoGotchiGameFrontEnd.DAL.Models.Pets;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class PetService : BaseService
    {
        private string _clientName;
        public PetService(IHttpClientFactory httpClientFactory, string? accessToken) : base(httpClientFactory, accessToken)
        {
            _clientName = "Pets";
        }


        public async Task<IEnumerable<Pet>> GetPets(PetSorter? sorter = null, PetFiltrator? filtrator = null)
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

            IEnumerable<Pet> pets = new List<Pet>();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                pets = await JsonSerializer.DeserializeAsync<IEnumerable<Pet>>(contentStream, options);
            }
            return pets;
        }

        public async Task<IEnumerable<Pet>> GetPetsPage(int pageSize, int pageNumber, PetSorter sorter, PetFiltrator filtrator)
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

            IEnumerable<Pet> pets = new List<Pet>();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                pets = await JsonSerializer.DeserializeAsync<IEnumerable<Pet>>(contentStream, options);
            }
            return pets;
        }
        public async Task<Pet?> GetPetById(int id)
        {
            var httpClient = GetHttpClient(_clientName);
            var httpResponseMessage = await httpClient.GetAsync(httpClient.BaseAddress + $"/{id}");

            Pet? Pet = null;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Pet = await JsonSerializer.DeserializeAsync<Pet>(contentStream, options);
            }
            return Pet;
        }

        public async Task<ServiceRezult> Create(AddPetModel addModel)
        {
            var httpClient = GetHttpClient(_clientName);
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PostAsync("", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdatePet(UpdatePetModel updateModel)
        {
            var httpClient = GetHttpClient(_clientName);

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PutAsync(httpClient.BaseAddress + "/data", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> Feed(int petId, int feederId)
        {
            var httpClient = GetHttpClient(_clientName);
            var parameters = new Dictionary<string, string>();
            parameters["feederId"] = feederId.ToString();

            var httpResponseMessage = await httpClient.PutAsync(httpClient.BaseAddress + $"/{petId}/feed", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> GiveDrink(int petId, int drinkerId)
        {
            var httpClient = GetHttpClient(_clientName);
            var parameters = new Dictionary<string, string>();
            parameters["drinkerId"] = drinkerId.ToString();

            var httpResponseMessage = await httpClient.PutAsync(httpClient.BaseAddress + $"/{petId}/drink", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }
        //public async Task<ServiceRezult> DeleteById(int PetId)
        //{
        //    var httpClient = GetHttpClient(_clientName);

        //    var httpResponseMessage = await httpClient.DeleteAsync(httpClient.BaseAddress + $"/{PetId}");

        //    return await GetCommandRezult(httpResponseMessage);
        //}
    }
}

using InnoGotchiGameFrontEnd.DAL.Models.Pets;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class PetService : BaseService
    {
        public PetService(HttpClient client) : base(client)
		{
			var apiControllerName = "pets";
			RequestClient.BaseAddress = new Uri(RequestClient.BaseAddress, apiControllerName);
		}


        public async Task<IEnumerable<Pet>> GetPets(PetSorter? sorter = null, PetFiltrator? filtrator = null)
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

            var httpResponseMessage = await RequestClient.GetAsync(RequestClient.BaseAddress + $"/{id}");

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

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PostAsync("", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdatePet(UpdatePetModel updateModel)
        {


            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PutAsync(RequestClient.BaseAddress + "/data", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> Feed(int petId, int feederId)
        {

            var parameters = new Dictionary<string, string>();
            parameters["feederId"] = feederId.ToString();

            var httpResponseMessage = await RequestClient.PutAsync(RequestClient.BaseAddress + $"/{petId}/feed", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> GiveDrink(int petId, int drinkerId)
        {

            var parameters = new Dictionary<string, string>();
            parameters["drinkerId"] = drinkerId.ToString();

            var httpResponseMessage = await RequestClient.PutAsync(RequestClient.BaseAddress + $"/{petId}/drink", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }
        //public async Task<ServiceRezult> DeleteById(int PetId)
        //{
        //    var RequestClient = GetHttpClient(_clientName);

        //    var httpResponseMessage = await RequestClient.DeleteAsync(RequestClient.BaseAddress + $"/{PetId}");

        //    return await GetCommandRezult(httpResponseMessage);
        //}
    }
}

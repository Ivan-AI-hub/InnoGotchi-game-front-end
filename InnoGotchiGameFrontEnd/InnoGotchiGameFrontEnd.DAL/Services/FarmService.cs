using InnoGotchiGameFrontEnd.DAL.Models.Farms;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class FarmService : BaseService
    {
        public FarmService(HttpClient client) : base(client)
		{
			var apiControllerName = "farms";
			RequestClient.BaseAddress = new Uri(RequestClient.BaseAddress, apiControllerName);
		}
        public async Task<IEnumerable<PetFarm>> GetFarms(FarmSorter? sorter = null, FarmFiltrator? filtrator = null)
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

            IEnumerable<PetFarm> farms = new List<PetFarm>();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                farms = await JsonSerializer.DeserializeAsync<IEnumerable<PetFarm>>(contentStream, options);
            }
            return farms;
        }

        public async Task<PetFarm?> GetFarmById(int id)
        {

            var httpResponseMessage = await RequestClient.GetAsync(RequestClient.BaseAddress + $"/{id}");

            PetFarm? farm = null;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                farm = await JsonSerializer.DeserializeAsync<PetFarm>(contentStream, options);
            }
            return farm;
        }

        public async Task<ServiceRezult> Create(AddFarmModel addModel)
        {

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PostAsync("", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdateFarm(UpdateFarmModel updateModel)
        {


            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PutAsync("", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

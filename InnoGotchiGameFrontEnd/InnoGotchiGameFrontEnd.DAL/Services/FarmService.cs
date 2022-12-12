using System.Text.Json;
using System.Text;
using InnoGotchiGameFrontEnd.DAL.Models.Farms;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class FarmService : BaseService
    {
        private string _clientName;
        public FarmService(IHttpClientFactory httpClientFactory, string? accessToken) : base(httpClientFactory, accessToken)
        {
            _clientName = "Farms";
        }
        public async Task<IEnumerable<PetFarm>> GetFarms(FarmSorter? sorter = null, FarmFiltrator? filtrator = null)
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
            var httpClient = GetHttpClient(_clientName);
            var httpResponseMessage = await httpClient.GetAsync(httpClient.BaseAddress + $"/{id}");

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
            var httpClient = GetHttpClient(_clientName);
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PostAsync("", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdateFarm(UpdateFarmModel updateModel)
        {
            var httpClient = GetHttpClient(_clientName);

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PutAsync("", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

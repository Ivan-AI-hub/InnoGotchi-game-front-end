using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.DAL.Models.Farms;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class FarmService : BaseService
    {
        private Uri _baseUri;
        public FarmService(IAuthorizedClient client) : base(client)
        {
            var apiControllerName = "farms";
            _baseUri = new Uri(client.BaseAddress, apiControllerName);
        }
        public async Task<IEnumerable<PetFarm>> GetFarms(FarmSorter? sorter = null, FarmFiltrator? filtrator = null)
        {

            var requestUrl = new StringBuilder($"?sortField={sorter.SortRule}" +
                                   $"&isDescendingSort={sorter.IsDescendingSort}");

            if (!String.IsNullOrEmpty(filtrator.Name))
            {
                requestUrl.Append($"&Name={filtrator.Name}");
            }


            var farms = await (await RequestClient).GetFromJsonAsync<IEnumerable<PetFarm>>(_baseUri + requestUrl.ToString());

            if (farms == null)
            {
                throw new Exception("BadRequest in FarmService GetFarms");
            }
            return farms;
        }

        public async Task<PetFarm?> GetFarmById(int id)
        {
            PetFarm? farm = await (await RequestClient).GetFromJsonAsync<PetFarm>(_baseUri + $"/{id}");
            return farm;
        }

        public async Task<ServiceRezult> Create(AddFarmModel addModel)
        {

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri, jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdateFarm(UpdateFarmModel updateModel)
        {


            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri, jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

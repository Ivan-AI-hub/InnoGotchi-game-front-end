using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.Domain;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Sorters;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class FarmService : BaseService, IFarmService
    {
        private Uri _baseUri;
        public FarmService(IAuthorizedClient client, CancellationToken cancellationToken = default) : base(client)
        {
            var apiControllerName = "farms";
            _baseUri = new Uri(client.BaseAddress, apiControllerName);
        }
        public async Task<IEnumerable<PetFarm>> GetAsync(FarmSorter? sorter = null, FarmFiltrator? filtrator = null, CancellationToken cancellationToken = default)
        {

            var requestUrl = new StringBuilder($"?sortField={sorter.SortRule}" +
                                   $"&isDescendingSort={sorter.IsDescendingSort}");

            if (!String.IsNullOrEmpty(filtrator.Name))
            {
                requestUrl.Append($"&Name={filtrator.Name}");
            }


            var farms = await (await RequestClient).GetFromJsonAsync<IEnumerable<PetFarm>>(_baseUri + requestUrl.ToString(), cancellationToken);

            if (farms == null)
            {
                throw new Exception("BadRequest in FarmService GetFarms");
            }
            return farms;
        }

        public async Task<PetFarm?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            PetFarm? farm = await (await RequestClient).GetFromJsonAsync<PetFarm>(_baseUri + $"/{id}", cancellationToken);
            return farm;
        }

        public async Task<IServiceRezult> CreateAsync(AddFarmModel addModel, CancellationToken cancellationToken = default)
        {

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri, jsonContent, cancellationToken);

            return await GetCommandRezultAsync(httpResponseMessage);
        }

        public async Task<IServiceRezult> UpdateAsync(UpdateFarmModel updateModel, CancellationToken cancellationToken = default)
        {


            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri, jsonContent, cancellationToken);

            return await GetCommandRezultAsync(httpResponseMessage);
        }
    }
}

using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.Domain;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.ColaborationRequestAggregate;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class ColaborationRequestService : BaseService, IColaborationRequestService
    {
        private Uri _baseUri;
        public ColaborationRequestService(IAuthorizedClient client) : base(client)
        {
            var apiControllerName = "colaborators";
            _baseUri = new Uri(client.BaseAddress, apiControllerName);
        }

        public async Task<IServiceRezult> CreateAsync(int recipientId, CancellationToken cancellationToken = default)
        {

            var parameters = new Dictionary<string, string>();
            parameters["recipientId"] = recipientId.ToString();

            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri, new FormUrlEncodedContent(parameters), cancellationToken);

            return await GetCommandRezultAsync(httpResponseMessage);
        }

        public async Task<IServiceRezult> ConfirmAsync(int requestId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + $"/{requestId}/confirm", new FormUrlEncodedContent(parameters), cancellationToken);

            return await GetCommandRezultAsync(httpResponseMessage);
        }

        public async Task<IServiceRezult> RejectAsync(int requestId, CancellationToken cancellationToken = default)
        {

            var parameters = new Dictionary<string, string>();

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + $"/{requestId}/reject", new FormUrlEncodedContent(parameters), cancellationToken);

            return await GetCommandRezultAsync(httpResponseMessage);
        }

        public async Task<IServiceRezult> DeleteByIdAsync(int requestId, CancellationToken cancellationToken = default)
        {
            var httpResponseMessage = await (await RequestClient).DeleteAsync(_baseUri + $"/{requestId}", cancellationToken);

            return await GetCommandRezultAsync(httpResponseMessage);
        }
    }
}

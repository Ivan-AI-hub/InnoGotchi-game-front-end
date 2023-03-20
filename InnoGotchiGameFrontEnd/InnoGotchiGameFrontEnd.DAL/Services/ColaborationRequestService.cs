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

        public async Task<IServiceResult> CreateAsync(int recipientId, CancellationToken cancellationToken = default)
        {

            var parameters = new Dictionary<string, string>
            {
                ["recipientId"] = recipientId.ToString()
            };

            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri, new FormUrlEncodedContent(parameters), cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }

        public async Task<IServiceResult> ConfirmAsync(int requestId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();
            var requestUri = _baseUri + $"/{requestId}/confirm";
            var httpResponseMessage = await (await RequestClient).PutAsync(requestUri,
                                                                           new FormUrlEncodedContent(parameters),
                                                                           cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }

        public async Task<IServiceResult> RejectAsync(int requestId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();
            var requestUri = _baseUri + $"/{requestId}/reject";
            var httpResponseMessage = await (await RequestClient).PutAsync(requestUri,
                                                                           new FormUrlEncodedContent(parameters),
                                                                           cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }

        public async Task<IServiceResult> DeleteByIdAsync(int requestId, CancellationToken cancellationToken = default)
        {
            var httpResponseMessage = await (await RequestClient).DeleteAsync(_baseUri + $"/{requestId}", cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }
    }
}

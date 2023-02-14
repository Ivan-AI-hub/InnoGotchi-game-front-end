using AuthorizationInfrastructure.HttpClients;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class ColaborationRequestService : BaseService
    {
        private Uri _baseUri;
        public ColaborationRequestService(IAuthorizedClient client) : base(client)
        {
            var apiControllerName = "colaborators";
            _baseUri = new Uri(client.BaseAddress, apiControllerName);
        }

        public async Task<ServiceRezult> AddCollaborator(int recipientId)
        {

            var parameters = new Dictionary<string, string>();
            parameters["recipientId"] = recipientId.ToString();

            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri, new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> ConfirmRequest(int requestId)
        {

            var parameters = new Dictionary<string, string>();

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + $"/{requestId}/confirm", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> RejectRequest(int requestId)
        {

            var parameters = new Dictionary<string, string>();

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + $"/{requestId}/reject", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> DeleteById(int requestId)
        {
            var httpResponseMessage = await (await RequestClient).DeleteAsync(_baseUri + $"/{requestId}");

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

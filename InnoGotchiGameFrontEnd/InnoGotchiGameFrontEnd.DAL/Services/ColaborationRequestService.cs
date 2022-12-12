namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class ColaborationRequestService : BaseService
    {
        private string _clientName;
        public ColaborationRequestService(IHttpClientFactory httpClientFactory, string? accessToken) : base(httpClientFactory, accessToken)
        {
            _clientName = "Colaborators";
        }

        public async Task<ServiceRezult> AddCollaborator(int senderId, int recipientId)
        {
            var httpClient = GetHttpClient(_clientName);
            var parameters = new Dictionary<string, string>();
            parameters["senderId"] = senderId.ToString();
            parameters["recipientId"] = recipientId.ToString();

            var httpResponseMessage = await httpClient.PostAsync("", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> ConfirmRequest(int requestId, int recipientId)
        {
            var httpClient = GetHttpClient(_clientName);
            var parameters = new Dictionary<string, string>();
            parameters["recipientId"] = recipientId.ToString();

            var httpResponseMessage = await httpClient.PutAsync(httpClient.BaseAddress + $"/{requestId}/confirm", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> RejectRequest(int requestId, int participantId)
        {
            var httpClient = GetHttpClient(_clientName);
            var parameters = new Dictionary<string, string>();
            parameters["participantId"] = participantId.ToString();

            var httpResponseMessage = await httpClient.PutAsync(httpClient.BaseAddress + $"/{requestId}/reject", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> DeleteById(int requestId)
        {
            var httpClient = GetHttpClient(_clientName);

            var httpResponseMessage = await httpClient.DeleteAsync(httpClient.BaseAddress + $"/{requestId}");

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

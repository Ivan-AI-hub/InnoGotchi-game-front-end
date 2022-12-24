namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class ColaborationRequestService : BaseService
    {
        private Uri _baseUri;
        public ColaborationRequestService(HttpClient client) : base(client)
		{
			var apiControllerName = "colaborators";
			_baseUri = new Uri(client.BaseAddress, apiControllerName);
		}

        public async Task<ServiceRezult> AddCollaborator(int senderId, int recipientId)
        {

            var parameters = new Dictionary<string, string>();
            parameters["senderId"] = senderId.ToString();
            parameters["recipientId"] = recipientId.ToString();

            var httpResponseMessage = await RequestClient.PostAsync(_baseUri, new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> ConfirmRequest(int requestId, int recipientId)
        {

            var parameters = new Dictionary<string, string>();
            parameters["recipientId"] = recipientId.ToString();

            var httpResponseMessage = await RequestClient.PutAsync(_baseUri + $"/{requestId}/confirm", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> RejectRequest(int requestId, int participantId)
        {

            var parameters = new Dictionary<string, string>();
            parameters["participantId"] = participantId.ToString();

            var httpResponseMessage = await RequestClient.PutAsync(_baseUri + $"/{requestId}/reject", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> DeleteById(int requestId)
        {


            var httpResponseMessage = await RequestClient.DeleteAsync(_baseUri + $"/{requestId}");

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

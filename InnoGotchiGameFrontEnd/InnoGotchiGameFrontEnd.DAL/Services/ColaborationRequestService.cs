namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class ColaborationRequestService : BaseService
    {
        public ColaborationRequestService(HttpClient client) : base(client)
		{
			var apiControllerName = "colaborators";
			RequestClient.BaseAddress = new Uri(RequestClient.BaseAddress, apiControllerName);
		}

        public async Task<ServiceRezult> AddCollaborator(int senderId, int recipientId)
        {

            var parameters = new Dictionary<string, string>();
            parameters["senderId"] = senderId.ToString();
            parameters["recipientId"] = recipientId.ToString();

            var httpResponseMessage = await RequestClient.PostAsync("", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> ConfirmRequest(int requestId, int recipientId)
        {

            var parameters = new Dictionary<string, string>();
            parameters["recipientId"] = recipientId.ToString();

            var httpResponseMessage = await RequestClient.PutAsync(RequestClient.BaseAddress + $"/{requestId}/confirm", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> RejectRequest(int requestId, int participantId)
        {

            var parameters = new Dictionary<string, string>();
            parameters["participantId"] = participantId.ToString();

            var httpResponseMessage = await RequestClient.PutAsync(RequestClient.BaseAddress + $"/{requestId}/reject", new FormUrlEncodedContent(parameters));

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> DeleteById(int requestId)
        {


            var httpResponseMessage = await RequestClient.DeleteAsync(RequestClient.BaseAddress + $"/{requestId}");

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

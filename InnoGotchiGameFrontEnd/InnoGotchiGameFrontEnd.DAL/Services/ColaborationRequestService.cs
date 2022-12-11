using InnoGotchiGameFrontEnd.DAL.Models.Users;
using System.Text.Json;
using System.Text;

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
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(new { senderId, recipientId }),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PostAsync("", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> ConfirmRequest(int requestId, int recipientId)
        {
            var httpClient = GetHttpClient(_clientName);
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(new {recipientId }),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PostAsync(httpClient.BaseAddress + $"{requestId}/confirm", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> RejectRequest(int requestId, int participantId)
        {
            var httpClient = GetHttpClient(_clientName);
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(new { participantId }),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PostAsync(httpClient.BaseAddress + $"{requestId}/reject", jsonContent);

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

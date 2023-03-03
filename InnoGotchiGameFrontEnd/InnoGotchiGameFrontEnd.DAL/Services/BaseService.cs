using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.Domain;
using InnoGotchiGameFrontEnd.Domain.ErrorModel;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class BaseService
    {
        protected Task<HttpClient> RequestClient => _client.GetHttpClientAsync();
        private IAuthorizedClient _client;

        public BaseService(IAuthorizedClient client)
        {
            _client = client;
        }

        protected async Task<IServiceResult> GetCommandResultAsync(HttpResponseMessage responseMessage)
        {
            var result = new ServiceResult();
            if (responseMessage.IsSuccessStatusCode)
            {
                return result;
            }

            using var contentStream = await responseMessage.Content.ReadAsStreamAsync();
            if (!contentStream.CanRead)
            {
                result.Errors.Add("Bad request");
                return result;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var error = await JsonSerializer.DeserializeAsync<ErrorDetails>(contentStream, options);
            result.Errors.AddRange(error.Message.Split('\n'));

            return result;
        }
    }
}

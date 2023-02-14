using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.DAL.Models.ErrorModel;
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

        protected async Task<ServiceRezult> GetCommandRezult(HttpResponseMessage responseMessage)
        {
            var rezult = new ServiceRezult();
            if (responseMessage.IsSuccessStatusCode)
            {
                return rezult;
            }
            else
            {
                using var contentStream = await responseMessage.Content.ReadAsStreamAsync();
                if (contentStream.CanRead)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var error = await JsonSerializer.DeserializeAsync<ErrorDetails>(contentStream, options);
                    rezult.Errors.AddRange(error.Message.Split('\n'));
                }
                else
                {
                    rezult.Errors.Add("Bad request");
                }
                return rezult;
            }
        }
    }
}

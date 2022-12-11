using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class BaseService
    {
        private IHttpClientFactory _httpClientFactory;
        protected string? AccessToken;


        public BaseService(IHttpClientFactory httpClientFactory, string? accessToken)
        {
            _httpClientFactory = httpClientFactory;
            AccessToken = accessToken;
        }

        protected HttpClient GetHttpClient(string clientName)
        {
            var httpClient = _httpClientFactory.CreateClient(clientName);
            if (AccessToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            return httpClient;
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
                if (contentStream.CanSeek)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    rezult.Errors.AddRange(await JsonSerializer.DeserializeAsync<List<string>>(contentStream, options));
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

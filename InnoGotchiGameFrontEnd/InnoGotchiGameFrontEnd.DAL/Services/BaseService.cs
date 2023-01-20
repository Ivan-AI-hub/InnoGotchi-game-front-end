using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class BaseService
    {
        protected HttpClient RequestClient;


        public BaseService(HttpClient client)
        {
            RequestClient = client;
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

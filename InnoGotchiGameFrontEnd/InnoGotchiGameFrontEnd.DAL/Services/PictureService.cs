using InnoGotchiGameFrontEnd.DAL.Models.Farms;
using System.Text.Json;
using System.Text;
using InnoGotchiGameFrontEnd.DAL.Models;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class PictureService : BaseService
    {
        string _clientName;
        public PictureService(IHttpClientFactory httpClientFactory, string? accessToken) : base(httpClientFactory, accessToken)
        {
            _clientName = "Pictures";
        }

        public async Task<IEnumerable<Picture>> GetPictures(string nameTemplate)
        {
            var httpClient = GetHttpClient(_clientName);
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var parameters = new Dictionary<string, string>();
            parameters["nameTemplate"] = nameTemplate;
            request.Content = new FormUrlEncodedContent(parameters);
            var httpResponseMessage = await httpClient.SendAsync(request);

            IEnumerable<Picture> pictures = new List<Picture>();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                pictures = await JsonSerializer.DeserializeAsync<IEnumerable<Picture>>(contentStream, options);
            }
            return pictures;
        }

        public async Task<Picture?> GetPictureById(int id)
        {
            var httpClient = GetHttpClient(_clientName);
            var httpResponseMessage = await httpClient.GetAsync(httpClient.BaseAddress + $"/{id}");

            Picture? picture = null;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                picture = await JsonSerializer.DeserializeAsync<Picture>(contentStream, options);
            }
            return picture;
        }

        public async Task<ServiceRezult> Create(Picture picture)
        {
            var httpClient = GetHttpClient(_clientName);
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(picture),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PostAsync("", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdateFarm(int updatedId,Picture picture)
        {
            var httpClient = GetHttpClient(_clientName);

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(picture),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PutAsync(httpClient.BaseAddress + $"/{updatedId}", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

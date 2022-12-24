using InnoGotchiGameFrontEnd.DAL.Models.Farms;
using System.Text.Json;
using System.Text;
using InnoGotchiGameFrontEnd.DAL.Models;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class PictureService : BaseService
    {
        private Uri _baseUri;
        public PictureService(HttpClient client) : base(client)
		{
			var apiControllerName = "pictures";
			_baseUri = new Uri(client.BaseAddress, apiControllerName);
		}

        public async Task<IEnumerable<Picture>> GetPictures(string nameTemplate)
        {

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _baseUri);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var parameters = new Dictionary<string, string>();
            parameters["nameTemplate"] = nameTemplate;
            request.Content = new FormUrlEncodedContent(parameters);
            var httpResponseMessage = await RequestClient.SendAsync(request);

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

            var httpResponseMessage = await RequestClient.GetAsync(_baseUri + $"/{id}");

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

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(picture),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PostAsync(_baseUri, jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdatePicture(int updatedId, Picture picture)
        {
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(picture),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PutAsync(_baseUri + $"/{updatedId}", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

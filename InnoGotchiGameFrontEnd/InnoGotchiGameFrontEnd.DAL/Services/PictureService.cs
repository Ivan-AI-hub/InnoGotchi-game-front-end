using InnoGotchiGameFrontEnd.DAL.Models.Farms;
using System.Text.Json;
using System.Text;
using InnoGotchiGameFrontEnd.DAL.Models;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class PictureService : BaseService
    {
        string _clientName;
        public PictureService(HttpClient client) : base(client)
		{
			var apiControllerName = "pictures";
			RequestClient.BaseAddress = new Uri(RequestClient.BaseAddress, apiControllerName);
		}

        public async Task<IEnumerable<Picture>> GetPictures(string nameTemplate)
        {

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, RequestClient.BaseAddress);

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

            var httpResponseMessage = await RequestClient.GetAsync(RequestClient.BaseAddress + $"/{id}");

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

            var httpResponseMessage = await RequestClient.PostAsync("", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdatePicture(int updatedId, Picture picture)
        {
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(picture),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await RequestClient.PutAsync(RequestClient.BaseAddress + $"/{updatedId}", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

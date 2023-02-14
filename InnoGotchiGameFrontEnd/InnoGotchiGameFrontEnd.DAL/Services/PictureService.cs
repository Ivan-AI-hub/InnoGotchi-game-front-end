using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.DAL.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class PictureService : BaseService
    {
        private Uri _baseUri;
        public PictureService(IAuthorizedClient client) : base(client)
        {
            var apiControllerName = "pictures";
            _baseUri = new Uri(client.BaseAddress, apiControllerName);
        }

        public async Task<IEnumerable<Picture>?> GetPictures(PictureFiltrator filtrator)
        {
            var requestUrl = new StringBuilder($"?");

            if (!String.IsNullOrEmpty(filtrator.Name))
            {
                requestUrl.Append($"&Name={filtrator.Name}");
            }
            if (!String.IsNullOrEmpty(filtrator.Description))
            {
                requestUrl.Append($"&Description={filtrator.Description}");
            }
            if (!String.IsNullOrEmpty(filtrator.Format))
            {
                requestUrl.Append($"&Format={filtrator.Format}");
            }
            var pictures = await (await RequestClient).GetFromJsonAsync<IEnumerable<Picture>>(_baseUri + requestUrl.ToString());

            if (pictures == null)
            {
                throw new Exception("BadRequest in PictureService GetPictures");
            }
            return pictures;
        }

        public async Task<Picture?> GetPictureById(int id)
        {
            Picture? picture = await (await RequestClient).GetFromJsonAsync<Picture>(_baseUri + $"/{id}");

            return picture;
        }

        public async Task<ServiceRezult> Create(Picture picture)
        {

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(picture),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri, jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }

        public async Task<ServiceRezult> UpdatePicture(int updatedId, Picture picture)
        {
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(picture),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + $"/{updatedId}", jsonContent);

            return await GetCommandRezult(httpResponseMessage);
        }
    }
}

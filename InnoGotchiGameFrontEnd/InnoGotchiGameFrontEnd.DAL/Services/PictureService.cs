using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.DAL.UriConstructors;
using InnoGotchiGameFrontEnd.Domain;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PictureAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class PictureService : BaseService, IPictureService
    {
        private Uri _baseUri;
        public PictureService(IAuthorizedClient client, CancellationToken cancellationToken = default) : base(client)
        {
            var apiControllerName = "pictures";
            _baseUri = new Uri(client.BaseAddress, apiControllerName);
        }

        public async Task<IEnumerable<Picture>?> GetAsync(PictureFiltrator filtrator, CancellationToken cancellationToken = default)
        {
            var query = PictureUriConstructor.GenerateUriQuery(filtrator);
            var pictures = await (await RequestClient).GetFromJsonAsync<IEnumerable<Picture>>(_baseUri + query, cancellationToken);

            if (pictures == null)
            {
                throw new Exception("BadRequest in PictureService GetPictures");
            }
            return pictures;
        }

        public async Task<Picture?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var requestUri = _baseUri + $"/{id}";
            Picture? picture = await (await RequestClient).GetFromJsonAsync<Picture>(requestUri, cancellationToken);

            return picture;
        }

        public async Task<IServiceResult> CreateAsync(Picture picture, CancellationToken cancellationToken = default)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(picture),Encoding.UTF8,"application/json");

            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri, jsonContent, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }

        public async Task<IServiceResult> UpdateAsync(int updatedId, Picture picture, CancellationToken cancellationToken = default)
        {
            using StringContent jsonContent = new( JsonSerializer.Serialize(picture),Encoding.UTF8,"application/json");
            var requestUri = _baseUri + $"/{updatedId}";

            var httpResponseMessage = await (await RequestClient).PutAsync(requestUri, jsonContent, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }
    }
}

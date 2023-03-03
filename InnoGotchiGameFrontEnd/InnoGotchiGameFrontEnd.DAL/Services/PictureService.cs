using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.Domain;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PictureAggregate;
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
            var pictures = await (await RequestClient).GetFromJsonAsync<IEnumerable<Picture>>(_baseUri + requestUrl.ToString(), cancellationToken);

            if (pictures == null)
            {
                throw new Exception("BadRequest in PictureService GetPictures");
            }
            return pictures;
        }

        public async Task<Picture?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            Picture? picture = await (await RequestClient).GetFromJsonAsync<Picture>(_baseUri + $"/{id}", cancellationToken);

            return picture;
        }

        public async Task<IServiceResult> CreateAsync(Picture picture, CancellationToken cancellationToken = default)
        {

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(picture),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PostAsync(_baseUri, jsonContent, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }

        public async Task<IServiceResult> UpdateAsync(int updatedId, Picture picture, CancellationToken cancellationToken = default)
        {
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(picture),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await (await RequestClient).PutAsync(_baseUri + $"/{updatedId}", jsonContent, cancellationToken);

            return await GetCommandResultAsync(httpResponseMessage);
        }
    }
}

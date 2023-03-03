using AuthorizationInfrastructure.HttpClients;
using AutoMapper;
using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.DAL.Models;
using InnoGotchiGameFrontEnd.DAL.Services;
using Microsoft.AspNetCore.Http;

namespace InnoGotchiGameFrontEnd.BLL
{
    public class PictureManager
    {
        private PictureService _pictureService;
        private IMapper _mapper;

        public PictureManager(IAuthorizedClient client, IMapper mapper)
        {
            _pictureService = new PictureService(client);
            _mapper = mapper;
        }

        public async Task<IEnumerable<PictureDTO>> GetAsync(PictureDTOFiltrator filtrator, CancellationToken cancellationToken = default)
        {
            var dataFiltrator = _mapper.Map<PictureFiltrator>(filtrator);
            var dataPictures = await _pictureService.GetAsync(dataFiltrator, cancellationToken);
            var pictures = _mapper.Map<IEnumerable<PictureDTO>>(dataPictures);
            return pictures;
        }

        public async Task<PictureDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var dataPsers = await _pictureService.GetByIdAsync(id, cancellationToken);
            var picture = _mapper.Map<PictureDTO>(dataPsers);
            return picture;
        }

        public async Task<ManagerRezult> CreateAsync(IFormFile file, string nameTemplate, CancellationToken cancellationToken = default)
        {
            var pictureData = new Picture()
            {
                Image = GetByteArrayFromImage(file),
                Name = nameTemplate + "-" + Guid.NewGuid().ToString()
            };
            var rezult = new ManagerRezult();
            var serviceRezult = await _pictureService.CreateAsync(pictureData, cancellationToken);
            rezult.Errors.AddRange(serviceRezult.Errors);

            return rezult;
        }

        public async Task<ManagerRezult> UpdateAsync(int updatedId, IFormFile file, CancellationToken cancellationToken = default)
        {
            var pictureData = await _pictureService.GetByIdAsync(updatedId, cancellationToken);
            pictureData.Image = GetByteArrayFromImage(file);
            var rezult = new ManagerRezult();
            var serviceRezult = await _pictureService.UpdateAsync(updatedId, pictureData,cancellationToken);
            rezult.Errors.AddRange(serviceRezult.Errors);

            return rezult;
        }

        private byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }
    }
}

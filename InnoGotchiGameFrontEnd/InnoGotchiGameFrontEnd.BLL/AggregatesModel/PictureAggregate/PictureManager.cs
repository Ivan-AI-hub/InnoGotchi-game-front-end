using AutoMapper;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PictureAggregate;
using Microsoft.AspNetCore.Http;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.PictureAggregate
{
    public class PictureManager
    {
        private IPictureService _pictureService;
        private IMapper _mapper;

        public PictureManager(IPictureService pictureService, IMapper mapper)
        {
            _pictureService = pictureService;
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

        public async Task<ManagerResult> CreateAsync(IFormFile file, string nameTemplate, CancellationToken cancellationToken = default)
        {
            var pictureData = new Picture()
            {
                Image = GetByteArrayFromImage(file),
                Name = nameTemplate + "-" + Guid.NewGuid().ToString()
            };
            var result = new ManagerResult();
            var serviceResult = await _pictureService.CreateAsync(pictureData, cancellationToken);
            result.Errors.AddRange(serviceResult.Errors);

            return result;
        }

        public async Task<ManagerResult> UpdateAsync(int updatedId, IFormFile file, CancellationToken cancellationToken = default)
        {
            var pictureData = await _pictureService.GetByIdAsync(updatedId, cancellationToken);
            pictureData.Image = GetByteArrayFromImage(file);
            var result = new ManagerResult();
            var serviceResult = await _pictureService.UpdateAsync(updatedId, pictureData, cancellationToken);
            result.Errors.AddRange(serviceResult.Errors);

            return result;
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

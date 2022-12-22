﻿using AutoMapper;
using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.DAL.Services;
using InnoGotchiGameFrontEnd.DAL.Models;
using Microsoft.AspNetCore.Http;
using InnoGotchiGameFrontEnd.BLL.Model.Identity;

namespace InnoGotchiGameFrontEnd.BLL
{
	public class PictureManager
    {
        private PictureService _service;
        private IMapper _mapper;

        public PictureManager(SecurityToken model, IHttpClientFactory clientFactory, IMapper mapper)
        {
            _service = new PictureService(clientFactory, model.AccessToken);
            _mapper = mapper;
        }

        public async Task<IEnumerable<PictureDTO>> GetAllPictures(string nameTemplate)
        {
            var dataPictures = await _service.GetPictures(nameTemplate);
            var pictures = _mapper.Map<IEnumerable<PictureDTO>>(dataPictures);
            return pictures;
        }

        public async Task<PictureDTO> GetPictureById(int id)
        {
            var dataPsers = await _service.GetPictureById(id);
            var picture = _mapper.Map<PictureDTO>(dataPsers);
            return picture;
        }

        public async Task<ManagerRezult> Create(IFormFile file, string nameTemplate)
        {
            var pictureData = new Picture()
            {
                Image = GetByteArrayFromImage(file),
                Name = nameTemplate + "-" + Guid.NewGuid().ToString()
            };
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.Create(pictureData);
            rezult.Errors.AddRange(serviceRezult.Errors);

            return rezult;
        }

        public async Task<ManagerRezult> UpdatePicture(int updatedId, IFormFile file)
        {
            var pictureData = await _service.GetPictureById(updatedId);
            pictureData.Image = GetByteArrayFromImage(file);
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.UpdateFarm(updatedId, pictureData);
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

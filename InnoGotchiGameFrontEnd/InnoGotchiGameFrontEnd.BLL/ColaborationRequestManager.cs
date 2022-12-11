﻿using InnoGotchiGameFrontEnd.BLL.Model.Authorize;
using InnoGotchiGameFrontEnd.DAL.Services;
using Microsoft.Extensions.Caching.Memory;

namespace InnoGotchiGameFrontEnd.BLL
{
    public class ColaborationRequestManager
    {
        private ColaborationRequestService _service;
        private IMemoryCache _cache;
        private AuthorizeModel _authorizeModel;

        public ColaborationRequestManager(AuthorizeModel model, IHttpClientFactory clientFactory, IMemoryCache cache)
        {
            _service = new ColaborationRequestService(clientFactory, model.AccessToken);
            _authorizeModel = model;
            _cache = cache;
        }

        public async Task<ManagerRezult> AddCollaborator(int recipientId)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.AddCollaborator(_authorizeModel.User.Id, recipientId);
            rezult.Errors.AddRange(serviceRezult.Errors);
            if (rezult.IsComplete) _cache.Remove("AuthodizedUser");
            return rezult;
        }

        public async Task<ManagerRezult> ConfirmRequest(int requestId)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.ConfirmRequest(requestId, _authorizeModel.User.Id);
            rezult.Errors.AddRange(serviceRezult.Errors);
            if (rezult.IsComplete) _cache.Remove("AuthodizedUser");
            return rezult;
        }

        public async Task<ManagerRezult> RejectRequest(int requestId)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.RejectRequest(requestId, _authorizeModel.User.Id);
            rezult.Errors.AddRange(serviceRezult.Errors);
            if (rezult.IsComplete) _cache.Remove("AuthodizedUser");
            return rezult;
        }

        public async Task<ManagerRezult> DeleteById(int requestId)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.DeleteById(requestId);
            rezult.Errors.AddRange(serviceRezult.Errors);
            if (rezult.IsComplete) _cache.Remove("AuthodizedUser");
            return rezult;
        }
    }
}

using InnoGotchiGameFrontEnd.BLL.Model.Authorize;
using InnoGotchiGameFrontEnd.DAL.Services;

namespace InnoGotchiGameFrontEnd.BLL
{
    public class ColaborationRequestManager
    {
        private ColaborationRequestService _service;
        private AuthorizeModel _authorizeModel;

        public ColaborationRequestManager(AuthorizeModel model, IHttpClientFactory clientFactory)
        {
            _service = new ColaborationRequestService(clientFactory, model.AccessToken);
            _authorizeModel = model;
        }

        public async Task<ManagerRezult> AddCollaborator(int recipientId)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.AddCollaborator(_authorizeModel.User.Id, recipientId);
            rezult.Errors.AddRange(serviceRezult.Errors);
            return rezult;
        }

        public async Task<ManagerRezult> ConfirmRequest(int requestId)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.ConfirmRequest(requestId, _authorizeModel.User.Id);
            rezult.Errors.AddRange(serviceRezult.Errors);
            return rezult;
        }

        public async Task<ManagerRezult> RejectRequest(int requestId)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.RejectRequest(requestId, _authorizeModel.User.Id);
            rezult.Errors.AddRange(serviceRezult.Errors);
            return rezult;
        }

        public async Task<ManagerRezult> DeleteById(int requestId)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.DeleteById(requestId);
            rezult.Errors.AddRange(serviceRezult.Errors);
            return rezult;
        }
    }
}

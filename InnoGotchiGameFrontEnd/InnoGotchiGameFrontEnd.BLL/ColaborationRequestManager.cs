using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.DAL.Services;
using System.Reflection;

namespace InnoGotchiGameFrontEnd.BLL
{
	public class ColaborationRequestManager
    {
        private ColaborationRequestService _service;
        private UserManager _userManager;

        public ColaborationRequestManager(HttpClient client, UserManager userManager)
        {
            _service = new ColaborationRequestService(client);
            _userManager = userManager;
        }

        public async Task<ManagerRezult> AddCollaborator(UserDTO sender, UserDTO recipient)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.AddCollaborator(recipient.Id);
            rezult.Errors.AddRange(serviceRezult.Errors);
            if (rezult.IsComplete)
            {
                var request = new ColaborationRequestDTO()
                {
                    RequestSender = sender,
                    RequestSenderId = sender.Id,
                    RequestReceiver = recipient,
                    RequestReceiverId = recipient.Id
                };
                var requests = recipient.UnconfirmedRequests.ToList();
                requests.Add(request);
                recipient.UnconfirmedRequests = requests;
            }
            return rezult;
        }

        public async Task<ManagerRezult> ConfirmRequest(int requestId, UserDTO recipient)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.ConfirmRequest(requestId);
            rezult.Errors.AddRange(serviceRezult.Errors);
            if (rezult.IsComplete)
            {
                var request = recipient.UnconfirmedRequests.First(x => x.Id == requestId);
                recipient.UnconfirmedRequests = recipient.UnconfirmedRequests.Where(x => x.Id != requestId);

                var colaborators = recipient.Collaborators.ToList();
                colaborators.Add(request.RequestSender);
                recipient.Collaborators = colaborators;
            }
            return rezult;
        }

        public async Task<ManagerRezult> RejectRequest(int requestId, UserDTO participant)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.RejectRequest(requestId);
            rezult.Errors.AddRange(serviceRezult.Errors);
            if (rezult.IsComplete)
            {
                var request = participant.UnconfirmedRequests.First(x => x.Id == requestId);
                participant.UnconfirmedRequests = participant.UnconfirmedRequests.Where(x => x.Id != requestId);
            }
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

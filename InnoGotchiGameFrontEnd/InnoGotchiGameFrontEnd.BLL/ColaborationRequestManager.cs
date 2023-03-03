using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.DAL.Services;

namespace InnoGotchiGameFrontEnd.BLL
{
    public class ColaborationRequestManager
    {
        private ColaborationRequestService _requestService;

        public ColaborationRequestManager(IAuthorizedClient client)
        {
            _requestService = new ColaborationRequestService(client);
        }

        public async Task<ManagerRezult> AddCollaboratorAsync(UserDTO sender, UserDTO recipient, CancellationToken cancellationToken = default)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _requestService.CreateAsync(recipient.Id, cancellationToken);
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

        public async Task<ManagerRezult> ConfirmAsync(int requestId, UserDTO recipient, CancellationToken cancellationToken = default)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _requestService.ConfirmAsync(requestId, cancellationToken);
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

        public async Task<ManagerRezult> RejectAsync(int requestId, UserDTO participant, CancellationToken cancellationToken = default)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _requestService.RejectAsync(requestId, cancellationToken);
            rezult.Errors.AddRange(serviceRezult.Errors);
            if (rezult.IsComplete)
            {
                var request = participant.UnconfirmedRequests.First(x => x.Id == requestId);
                participant.UnconfirmedRequests = participant.UnconfirmedRequests.Where(x => x.Id != requestId);
            }
            return rezult;
        }

        public async Task<ManagerRezult> DeleteByIdAsync(int requestId, CancellationToken cancellationToken = default)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _requestService.DeleteByIdAsync(requestId, cancellationToken);
            rezult.Errors.AddRange(serviceRezult.Errors);
            return rezult;
        }
    }
}

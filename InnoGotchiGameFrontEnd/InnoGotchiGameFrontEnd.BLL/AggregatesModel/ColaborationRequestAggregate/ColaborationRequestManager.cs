using InnoGotchiGameFrontEnd.BLL.AggregatesModel.UserAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.ColaborationRequestAggregate;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.ColaborationRequestAggregate
{
    public class ColaborationRequestManager
    {
        private IColaborationRequestService _requestService;

        public ColaborationRequestManager(IColaborationRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<ManagerResult> AddCollaboratorAsync(UserDTO sender, UserDTO recipient, CancellationToken cancellationToken = default)
        {
            var serviceResult = await _requestService.CreateAsync(recipient.Id, cancellationToken);
            if (!serviceResult.IsComplete)
            {
                return new ManagerResult(serviceResult);
            }

            var request = new ColaborationRequestDTO(0, ColaborationRequestStatusDTO.Undefined, sender, recipient);
            recipient.UnconfirmedRequests.Add(request);
            return new ManagerResult();
        }

        public async Task<ManagerResult> ConfirmAsync(int requestId, UserDTO recipient, CancellationToken cancellationToken = default)
        {
            var serviceResult = await _requestService.ConfirmAsync(requestId, cancellationToken);
            if (!serviceResult.IsComplete)
            {
                return new ManagerResult(serviceResult);
            }

            recipient.ConfirmRequest(requestId);
            return new ManagerResult();
        }

        public async Task<ManagerResult> RejectAsync(int requestId, UserDTO participant, CancellationToken cancellationToken = default)
        {
            var serviceResult = await _requestService.RejectAsync(requestId, cancellationToken);
            if (!serviceResult.IsComplete)
            {
                return new ManagerResult(serviceResult);
            }

            participant.RejectRequest(requestId);
            return new ManagerResult();
        }

        public async Task<ManagerResult> DeleteByIdAsync(int requestId, CancellationToken cancellationToken = default)
        {
            var serviceResult = await _requestService.DeleteByIdAsync(requestId, cancellationToken);
            return new ManagerResult(serviceResult);
        }
    }
}

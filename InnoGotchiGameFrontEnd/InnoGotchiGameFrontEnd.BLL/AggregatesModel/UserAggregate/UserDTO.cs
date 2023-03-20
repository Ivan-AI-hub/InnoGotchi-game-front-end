using InnoGotchiGameFrontEnd.BLL.AggregatesModel.ColaborationRequestAggregate;
using InnoGotchiGameFrontEnd.BLL.AggregatesModel.FarmAggregate;
using InnoGotchiGameFrontEnd.BLL.AggregatesModel.PictureAggregate;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.UserAggregate
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int OwnPetFarmId { get; set; }
        public PictureDTO? Picture { get; set; }
        public PetFarmDTO? OwnPetFarm { get; set; }

        public IList<ColaborationRequestDTO> UnconfirmedRequests { get; set; }
        public IList<ColaborationRequestDTO> RejectedRequests { get; set; }
        public IList<UserDTO> Collaborators { get; set; }

        public int MessagesCount => UnconfirmedRequests.Count() + RejectedRequests.Count();

        public UserDTO()
        {
            UnconfirmedRequests = new List<ColaborationRequestDTO>();
            RejectedRequests = new List<ColaborationRequestDTO>();
            Collaborators = new List<UserDTO>();
        }

        public void RejectRequest(int requestId)
        {
            var request = UnconfirmedRequests.First(x => x.Id == requestId);
            UnconfirmedRequests.Remove(request);
        }
        public void ConfirmRequest(int requestId)
        {
            var request = UnconfirmedRequests.First(x => x.Id == requestId);
            UnconfirmedRequests.Remove(request);
            Collaborators.Add(request.RequestSender);
        }
    }
}

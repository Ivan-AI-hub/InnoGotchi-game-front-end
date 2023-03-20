using InnoGotchiGameFrontEnd.Domain.AggregatesModel.ColaborationRequestAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PictureAggregate;

namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Picture? Picture { get; set; }
        public int OwnPetFarmId { get; set; }
        public PetFarm? OwnPetFarm { get; set; }

        public IEnumerable<ColaborationRequest> UnconfirmedRequests { get; set; }
        public IEnumerable<ColaborationRequest> RejectedRequests { get; set; }
        public IEnumerable<User> Collaborators { get; set; }

        public User()
        {
            UnconfirmedRequests = new List<ColaborationRequest>();
            RejectedRequests = new List<ColaborationRequest>();
            Collaborators = new List<User?>();
        }
    }
}

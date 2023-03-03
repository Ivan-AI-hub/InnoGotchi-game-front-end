namespace InnoGotchiGameFrontEnd.BLL.Model
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
    }
}

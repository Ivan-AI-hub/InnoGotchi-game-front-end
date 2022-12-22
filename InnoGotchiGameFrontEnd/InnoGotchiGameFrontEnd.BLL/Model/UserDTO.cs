namespace InnoGotchiGameFrontEnd.BLL.Model
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public PictureDTO? Picture { get; set; }

        public PetFarmDTO? OwnPetFarm { get; set; }

        public IEnumerable<ColaborationRequestDTO> UnconfirmedRequest { get; set; }
        public IEnumerable<PetFarmDTO?> CollaboratedFarms { get; set; }
        public IEnumerable<int> CollaboratersId { get; set; }

        public UserDTO()
        {
            UnconfirmedRequest = new List<ColaborationRequestDTO>();
            CollaboratedFarms = new List<PetFarmDTO?>();
        }
    }
}

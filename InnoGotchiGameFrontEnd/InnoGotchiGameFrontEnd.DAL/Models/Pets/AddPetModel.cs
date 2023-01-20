namespace InnoGotchiGameFrontEnd.DAL.Models.Pets
{
    public class AddPetModel
    {
        public int FarmId { get; set; }
        public string Name { get; set; }
        public PetView View { get; set; }

        public AddPetModel()
        {
            View = new PetView();
        }
    }
}

namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Comands
{
    public class AddPetModel
    {
        public int FarmId { get; set; }
        public string Name { get; set; }
        public PetView View { get; set; }

        public AddPetModel(int farmId, string name, PetView view)
        {
            FarmId = farmId;
            Name = name;
            View = view;
        }
    }
}

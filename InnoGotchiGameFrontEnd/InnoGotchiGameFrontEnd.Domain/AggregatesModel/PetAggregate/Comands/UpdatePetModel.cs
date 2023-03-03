namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Comands
{
    public class UpdatePetModel
    {
        public int UpdatedId { get; set; }
        public string Name { get; set; }

        public UpdatePetModel(int updatedId, string name)
        {
            UpdatedId = updatedId;
            Name = name;
        }
    }
}

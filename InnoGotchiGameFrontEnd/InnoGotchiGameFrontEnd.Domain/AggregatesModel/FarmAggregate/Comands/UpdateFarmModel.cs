namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Comands
{
    public class UpdateFarmModel
    {
        public int UpdatedId { get; set; }
        public string Name { get; set; }

        public UpdateFarmModel(int updatedId, string name)
        {
            UpdatedId = updatedId;
            Name = name;
        }
    }
}

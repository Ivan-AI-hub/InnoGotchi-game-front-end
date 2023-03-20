namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Comands
{
    public class AddFarmModel
    {
        public string Name { get; set; }

        public AddFarmModel(string name)
        {
            Name = name;
        }
    }
}

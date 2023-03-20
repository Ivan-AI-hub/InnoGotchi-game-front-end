using InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate.BaseModels;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate
{
    public class PetDTOFiltrator
    {
        public string Name { get; set; } = "";
        public int Age { get; set; } = 0;
        public HungerLevel? HungerLevel { get; set; }
        public ThirstyLevel? ThirstyLevel { get; set; }
    }
}

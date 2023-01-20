using InnoGotchiGameFrontEnd.BLL.Model.Pet;

namespace InnoGotchiGameFrontEnd.BLL.Filtrators
{
    public class PetDTOFiltrator
    {
        public string Name { get; set; } = "";
        public int Age { get; set; } = 0;
        public HungerLevel? HungerLevel { get; set; }
        public ThirstyLevel? ThirstyLevel { get; set; }
    }
}

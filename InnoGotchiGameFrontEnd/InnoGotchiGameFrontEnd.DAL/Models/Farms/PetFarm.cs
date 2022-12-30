using InnoGotchiGameFrontEnd.DAL.Models.Pets;
using System.Text.Json.Serialization;

namespace InnoGotchiGameFrontEnd.DAL.Models.Farms
{
    public class PetFarm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public int OwnerId { get; set; }

        public IEnumerable<Pet> Pets { get; set; }

        public PetFarm()
        {
            Pets = new List<Pet>();
        }
    }
}

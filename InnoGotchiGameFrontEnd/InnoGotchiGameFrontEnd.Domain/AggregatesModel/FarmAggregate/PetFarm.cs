using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate;

namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate
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

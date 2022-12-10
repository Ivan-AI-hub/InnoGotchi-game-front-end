namespace InnoGotchiGameFrontEnd.DAL.Models
{
    public class PetFarm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public int OwnerId { get; set; }

        public List<Pet> Pets { get; }

        public int AlivesPetsCount { get; set; }
        public int DeadsPetsCount { get; set; }
        public double AverageFeedingPeriod { get; set; }
        public double AverageDrinkingPeriod { get; set; }
        public double AveragePetsHappinessDaysCount { get; set; }
        public double AveragePetsAge { get; set; }
        public int FeedingCount { get; set; }
        public int DrinkingCount { get; set; }

        public PetFarm()
        {
            Pets = new List<Pet>();
        }
    }
}

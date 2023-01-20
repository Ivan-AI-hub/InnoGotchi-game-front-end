namespace InnoGotchiGameFrontEnd.DAL.Models.Pets
{
    public class PetFiltrator
    {
        public string Name { get; set; } = "";
        public int DaysAlive { get; set; } = 0;
        public DaysInterval? FeedingInterval { get; set; }
        public DaysInterval? DrinkingInterval { get; set; }
    }
}

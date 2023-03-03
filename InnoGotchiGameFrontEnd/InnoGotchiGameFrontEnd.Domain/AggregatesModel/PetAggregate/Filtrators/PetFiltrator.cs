namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Filtrators
{
    public class PetFiltrator
    {
        public string Name { get; set; }
        public int DaysAlive { get; set; }
        public DaysInterval? FeedingInterval { get; set; }
        public DaysInterval? DrinkingInterval { get; set; }
    }
}

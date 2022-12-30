namespace InnoGotchiGameFrontEnd.BLL.Model.Pet
{
    public record PetStatisticDTO
    {
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        public bool IsAlive { get; set; }
        public DateTime? DeadDate { get; set; }
        public int FeedingCount { get; set; }
        public int DrinkingCount { get; set; }
        public DateTime FirstHappinessDay { get; set; }
        public DateTime DateLastFeed { get; set; }
        public DateTime DateLastDrink { get; set; }

        public int Age { get; set; }
        public int HappinessDayCount { get; set; }
        public double AverageDrinkingPeriod { get; set; }
        public double AverageFeedingPeriod { get; set; }
        public HungerLevel HungerLevel => GetHungerLevel();
        public ThirstyLevel ThirstyLevel => GetThirstyLevel();

        private HungerLevel GetHungerLevel()
        {
            var dayCount = (DateTime.UtcNow - DateLastFeed).Days;
            
            switch(dayCount)
            {
                case 0: return HungerLevel.Full;

                case 1: 
                case 2: return HungerLevel.Normal;
                case 3: 
                case 4:
                case 5: return HungerLevel.Hunger;
                default: return HungerLevel.Dead;
            }
        }

        private ThirstyLevel GetThirstyLevel()
        {
            var dayCount = (DateTime.UtcNow - DateLastFeed).Days;

            switch (dayCount)
            {
                case 0: return ThirstyLevel.Full;

                case 1: 
                case 2: return ThirstyLevel.Normal;
                case 3:
                case 4:
                case 5: return ThirstyLevel.Thirsty;
                default: return ThirstyLevel.Dead;
            }
        }
    }
}

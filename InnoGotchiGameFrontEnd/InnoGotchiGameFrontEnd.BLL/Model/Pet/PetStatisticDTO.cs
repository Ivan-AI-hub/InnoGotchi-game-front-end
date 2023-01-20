namespace InnoGotchiGameFrontEnd.BLL.Model.Pet
{
    public record PetStatisticDTO
    {
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        internal bool IsAlive { get; set; }
        public AliveState AliveState => GetAliveState();
        public DateTime? DeadDate { get; set; }
        public int FeedingCount { get; set; }
        public int DrinkingCount { get; set; }
        public DateTime FirstHappinessDay { get; set; }
        public DateTime DateLastFeed { get; set; }
        public DateTime DateLastDrink { get; set; }

        public int Age => _daysAliveCount / 7;
        public int HappinessDayCount => IsAlive ? (DateTime.Now - FirstHappinessDay).Days : 0;
        public double AverageDrinkingPeriod => DrinkingCount != 0 ? _daysAliveCount / DrinkingCount : DrinkingCount;
        public double AverageFeedingPeriod => FeedingCount != 0 ? _daysAliveCount / FeedingCount : FeedingCount;

        public HungerLevel HungerLevel { get => GetHungerLevel(); set => _currentHungerLevel = value; }
        public ThirstyLevel ThirstyLevel { get => GetThirstyLevel(); set => _currentThirstyLevel = value; }
        private HungerLevel? _currentHungerLevel;
        private ThirstyLevel? _currentThirstyLevel;
        private int _daysAliveCount => IsAlive ? (DateTime.Now - BornDate).Days : (DeadDate!.Value - BornDate).Days;

        public PetStatisticDTO()
        {
            if (DeadDate == null)
            {
                IsAlive = true;
            }
            else
            {
                IsAlive = false;
            }

        }
        private AliveState GetAliveState()
        {
            if (IsAlive == false)
                return AliveState.Dead;
            else if (HungerLevel == HungerLevel.Dead)
            {
                DeadDate = DateLastFeed.AddDays(DateToHungerLevelConvertor.GetInterval(HungerLevel.Dead).MinDays);
                return AliveState.NotAnnouncedDead;
            }
            else if (ThirstyLevel == ThirstyLevel.Dead)
            {
                DeadDate = DateLastFeed.AddDays(DateToThirstyLevelConvertor.GetInterval(ThirstyLevel.Dead).MinDays);
                return AliveState.NotAnnouncedDead;
            }
            else
                return AliveState.Alive;
        }
        private HungerLevel GetHungerLevel()
        {
            if (_currentHungerLevel != null)
                return (HungerLevel)_currentHungerLevel;
            else
                return DateToHungerLevelConvertor.GetHungerLevel(DateLastFeed);
        }
        private ThirstyLevel GetThirstyLevel()
        {
            if (_currentThirstyLevel != null)
                return (ThirstyLevel)_currentThirstyLevel;
            else
                return DateToThirstyLevelConvertor.GetThirstyLevel(DateLastDrink);
        }
    }
}

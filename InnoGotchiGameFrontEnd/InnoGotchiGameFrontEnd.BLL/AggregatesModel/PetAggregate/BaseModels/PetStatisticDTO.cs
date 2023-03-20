namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate.BaseModels
{
    public record PetStatisticDTO
    {
        private HungerLevel? _currentHungerLevel;
        private ThirstyLevel? _currentThirstyLevel;

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
        public double AverageDrinkingPeriod => _daysAliveCount == 0 ? DrinkingCount : DrinkingCount / _daysAliveCount;
        public double AverageFeedingPeriod => _daysAliveCount == 0 ? FeedingCount : FeedingCount / _daysAliveCount;

        public HungerLevel HungerLevel { get => GetHungerLevel(); set => _currentHungerLevel = value; }
        public ThirstyLevel ThirstyLevel { get => GetThirstyLevel(); set => _currentThirstyLevel = value; }
        private int _daysAliveCount => IsAlive ? (DateTime.Now - BornDate).Days : (DeadDate!.Value - BornDate).Days;

        public PetStatisticDTO()
        {
            if (DeadDate == null)
            {
                IsAlive = true;
            }
        }
        public void Feed()
        {
            HungerLevel = HungerLevel.Full;
            FeedingCount++;
            DateLastFeed = DateTime.UtcNow;
        }

        public void GiveDrink()
        {
            ThirstyLevel = ThirstyLevel.Full;
            DrinkingCount++;
            DateLastDrink = DateTime.UtcNow;
        }

        public void SetDeadStatus()
        {
            IsAlive = false;
            DeadDate ??= DateTime.UtcNow;
        }
        public void ResetHappinesDay()
        {
            FirstHappinessDay = DateTime.UtcNow;
        }

        private AliveState GetAliveState()
        {
            if (IsAlive == false)
                return AliveState.Dead;
            if (HungerLevel == HungerLevel.Dead)
            {
                DeadDate = DateLastFeed.AddDays(DateToHungerLevelConvertor.GetInterval(HungerLevel.Dead).MinDays);
                return AliveState.NotAnnouncedDead;
            }
            if (ThirstyLevel == ThirstyLevel.Dead)
            {
                DeadDate = DateLastFeed.AddDays(DateToThirstyLevelConvertor.GetInterval(ThirstyLevel.Dead).MinDays);
                return AliveState.NotAnnouncedDead;
            }

            return AliveState.Alive;
        }
        private HungerLevel GetHungerLevel()
        {
            if (_currentHungerLevel != null)
                return (HungerLevel)_currentHungerLevel;

            return DateToHungerLevelConvertor.GetHungerLevel(DateLastFeed);
        }
        private ThirstyLevel GetThirstyLevel()
        {
            if (_currentThirstyLevel != null)
                return (ThirstyLevel)_currentThirstyLevel;

            return DateToThirstyLevelConvertor.GetThirstyLevel(DateLastDrink);
        }
    }
}

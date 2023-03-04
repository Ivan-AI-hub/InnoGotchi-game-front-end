using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate.BaseModels
{
    internal static class DateToThirstyLevelConvertor
    {
        static DaysInterval FullLevelInterval;
        static DaysInterval NormalLevelInterval;
        static DaysInterval ThirstyLevelInterval;
        static DaysInterval DeadLevelInterval;
        static DateToThirstyLevelConvertor()
        {
            FullLevelInterval = new DaysInterval(-1, 1);
            NormalLevelInterval = new DaysInterval(1, 2);
            ThirstyLevelInterval = new DaysInterval(3, 5);
            DeadLevelInterval = new DaysInterval(5, 365);
        }
        public static ThirstyLevel GetThirstyLevel(DateTime dateLastDrink)
        {
            if (FullLevelInterval.InInterval(dateLastDrink)) return ThirstyLevel.Full;
            if (NormalLevelInterval.InInterval(dateLastDrink)) return ThirstyLevel.Normal;
            if (ThirstyLevelInterval.InInterval(dateLastDrink)) return ThirstyLevel.Thirsty;
            return ThirstyLevel.Dead;
        }
        public static DaysInterval GetInterval(ThirstyLevel thirstyLevel)
        {
            switch (thirstyLevel)
            {
                case ThirstyLevel.Full: return FullLevelInterval;
                case ThirstyLevel.Normal: return NormalLevelInterval;
                case ThirstyLevel.Thirsty: return ThirstyLevelInterval;
                case ThirstyLevel.Dead: return DeadLevelInterval;
                default: return DeadLevelInterval;
            }
        }
    }
}

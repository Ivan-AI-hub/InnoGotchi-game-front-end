
namespace InnoGotchiGameFrontEnd.BLL.Model.Pet
{
    internal static class DateToThirstyLevelConvertor
    {
        static DaysInterval FullLevelInterval;
        static DaysInterval NormalLevelInterval;
        static DaysInterval ThirstyLevelInterval;
        static DaysInterval DeadLevelInterval;
        static DateToThirstyLevelConvertor()
        {
            FullLevelInterval = new DaysInterval(0, 1);
            NormalLevelInterval = new DaysInterval(1, 2);
            ThirstyLevelInterval = new DaysInterval(3, 5);
            DeadLevelInterval = new DaysInterval(5, 365);
        }
        public static ThirstyLevel GetThirstyLevel(DateTime DateLastFeed)
        {
            var dayCount = (DateTime.UtcNow - DateLastFeed).Days;

            if (FullLevelInterval.InInterval(dayCount)) return ThirstyLevel.Full;
            if (NormalLevelInterval.InInterval(dayCount)) return ThirstyLevel.Normal;
            if (ThirstyLevelInterval.InInterval(dayCount)) return ThirstyLevel.Thirsty;
            return ThirstyLevel.Dead;
        }
        public static DaysInterval GetInterval(ThirstyLevel ThirstyLevel)
        {
            if (ThirstyLevel == ThirstyLevel.Full) return FullLevelInterval;
            if (ThirstyLevel == ThirstyLevel.Normal) return NormalLevelInterval;
            if (ThirstyLevel == ThirstyLevel.Thirsty) return ThirstyLevelInterval;
            if (ThirstyLevel == ThirstyLevel.Dead) return DeadLevelInterval;
            return DeadLevelInterval;
        }
    }
}

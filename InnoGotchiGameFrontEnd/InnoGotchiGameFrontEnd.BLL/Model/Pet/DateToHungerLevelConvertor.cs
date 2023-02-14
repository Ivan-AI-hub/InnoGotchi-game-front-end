using InnoGotchiGameFrontEnd.DAL.Models;

namespace InnoGotchiGameFrontEnd.BLL.Model.Pet
{
    internal static class DateToHungerLevelConvertor
    {
        static DaysInterval FullLevelInterval;
        static DaysInterval NormalLevelInterval;
        static DaysInterval HungerLevelInterval;
        static DaysInterval DeadLevelInterval;
        static DateToHungerLevelConvertor()
        {
            FullLevelInterval = new DaysInterval(-1, 1);
            NormalLevelInterval = new DaysInterval(1, 2);
            HungerLevelInterval = new DaysInterval(3, 5);
            DeadLevelInterval = new DaysInterval(5, 365);
        }
        public static HungerLevel GetHungerLevel(DateTime DateLastFeed)
        {
            if (HungerLevelInterval.InInterval(DateLastFeed)) return HungerLevel.Hunger;
            if (NormalLevelInterval.InInterval(DateLastFeed)) return HungerLevel.Normal;
            if (FullLevelInterval.InInterval(DateLastFeed)) return HungerLevel.Full;

            return HungerLevel.Dead;
        }
        public static DaysInterval GetInterval(HungerLevel hungerLevel)
        {
            switch (hungerLevel)
            {
                case HungerLevel.Full: return FullLevelInterval;
                case HungerLevel.Normal: return NormalLevelInterval;
                case HungerLevel.Hunger: return HungerLevelInterval;
                case HungerLevel.Dead: return DeadLevelInterval;
                default: return DeadLevelInterval;
            }
        }
    }
}

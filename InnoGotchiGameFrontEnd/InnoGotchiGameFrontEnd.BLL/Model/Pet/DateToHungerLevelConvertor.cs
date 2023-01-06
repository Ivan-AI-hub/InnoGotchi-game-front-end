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
            FullLevelInterval = new DaysInterval(0, 1);
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
            if (hungerLevel == HungerLevel.Full) return FullLevelInterval;
            if (hungerLevel == HungerLevel.Normal) return NormalLevelInterval;
            if (hungerLevel == HungerLevel.Hunger) return HungerLevelInterval;
            if (hungerLevel == HungerLevel.Dead) return DeadLevelInterval;
            return DeadLevelInterval;
        }
    }
}

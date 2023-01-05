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
            var dayCount = (DateTime.UtcNow - DateLastFeed).Days;

            if (FullLevelInterval.InInterval(dayCount)) return HungerLevel.Full;
            if (NormalLevelInterval.InInterval(dayCount)) return HungerLevel.Normal;
            if (HungerLevelInterval.InInterval(dayCount)) return HungerLevel.Hunger;
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

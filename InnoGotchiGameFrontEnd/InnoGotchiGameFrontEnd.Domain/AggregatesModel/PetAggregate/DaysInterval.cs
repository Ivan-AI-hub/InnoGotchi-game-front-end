namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate
{
    public class DaysInterval
    {
        public int MinDays { get; set; }
        public int MaxDays { get; set; }
        private DateTime MinDate { get; set; }
        private DateTime MaxDate { get; set; }
        public DaysInterval(int minDays, int maxDays)
        {
            MinDays = minDays;
            MaxDays = maxDays;
            MinDate = (DateTime.UtcNow - new TimeSpan(MinDays, 0, 0, 0)).Date;
            MaxDate = (DateTime.UtcNow - new TimeSpan(MaxDays, 0, 0, 0)).Date;
        }
        public bool InInterval(int daysCount)
        {
            var date = (DateTime.UtcNow - new TimeSpan(daysCount, 0, 0, 0)).Date;
            return InInterval(date);
        }
        public bool InInterval(DateTime date)
        {
            return date <= MinDate && date > MaxDate;
        }
    }
}


namespace InnoGotchiGameFrontEnd.BLL.Model.Pet
{
    public class DaysInterval
    {
        public int MinDays { get; set; }
        public int MaxDays { get; set; }
        public DaysInterval(int minDays, int maxDays)
        {
            MinDays = minDays;
            MaxDays = maxDays;
        }
        public bool InInterval(int DaysCount)
        {
            return DaysCount >= MinDays && DaysCount < MaxDays;
        }
        public bool InInterval(DateTime date)
        {
            var daysCount = (DateTime.UtcNow - date).Days;
            return InInterval(daysCount);
        }
    }
}

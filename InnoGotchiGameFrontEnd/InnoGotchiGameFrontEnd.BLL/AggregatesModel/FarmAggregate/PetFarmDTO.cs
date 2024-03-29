﻿using InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate.BaseModels;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.FarmAggregate
{
    public class PetFarmDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public int OwnerId { get; set; }
        public List<PetDTO> Pets { get; }

        public int AlivesPetsCount => Pets.Count(x => x.Statistic.IsAlive);
        public int DeadsPetsCount => Pets.Count(x => !x.Statistic.IsAlive);
        public double AverageFeedingPeriod => Pets.Count == 0 ? 0 : Pets.Average(x => x.Statistic.AverageFeedingPeriod);
        public double AverageDrinkingPeriod => Pets.Count == 0 ? 0 : Pets.Average(x => x.Statistic.AverageDrinkingPeriod);
        public double AveragePetsHappinessDaysCount => Pets.Count == 0 ? 0 : Pets.Average(x => x.Statistic.HappinessDayCount);
        public double AveragePetsAge => Pets.Count == 0 ? 0 : Pets.Average(x => x.Statistic.Age);
        public int FeedingCount => Pets.Count == 0 ? 0 : Pets.Sum(x => x.Statistic.FeedingCount);
        public int DrinkingCount => Pets.Count == 0 ? 0 : Pets.Sum(x => x.Statistic.DrinkingCount);

        public PetFarmDTO()
        {
            Pets = new List<PetDTO>();
        }
    }
}

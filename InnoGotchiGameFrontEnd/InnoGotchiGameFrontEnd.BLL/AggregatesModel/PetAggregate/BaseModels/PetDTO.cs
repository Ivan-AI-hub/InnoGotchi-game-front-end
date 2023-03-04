﻿namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate.BaseModels
{
    public class PetDTO
    {
        public int Id { get; set; }

        public PetStatisticDTO Statistic { get; set; }
        public PetViewDTO View { get; set; }
        public int FarmId { get; set; }

        public PetDTO()
        {
            Statistic = new PetStatisticDTO();
            View = new PetViewDTO();
        }
    }
}

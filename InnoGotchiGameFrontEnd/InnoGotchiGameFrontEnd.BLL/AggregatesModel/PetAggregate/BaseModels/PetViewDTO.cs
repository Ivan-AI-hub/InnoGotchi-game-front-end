using InnoGotchiGameFrontEnd.BLL.AggregatesModel.PictureAggregate;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate.BaseModels
{
    public record PetViewDTO
    {
        public PictureDTO? Picture { get; set; }
    }
}

using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PictureAggregate;

namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate
{
    public record PetView
    {
        public Picture? Picture { get; set; }
    }
}

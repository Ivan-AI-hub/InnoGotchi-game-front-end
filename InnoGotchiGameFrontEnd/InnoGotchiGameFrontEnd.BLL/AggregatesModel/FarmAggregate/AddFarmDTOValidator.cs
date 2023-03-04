using FluentValidation;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.FarmAggregate
{
    internal class AddFarmDTOValidator : AbstractValidator<AddFarmDTOModel>
    {
        public AddFarmDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}

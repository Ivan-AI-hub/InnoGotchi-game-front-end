using FluentValidation;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.FarmAggregate
{
    internal class UpdateFarmDTOValidator : AbstractValidator<UpdateFarmDTOModel>
    {
        public UpdateFarmDTOValidator()
        {
            RuleFor(x => x.UpdatedId).NotNull().Must(x => x > 0);
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}

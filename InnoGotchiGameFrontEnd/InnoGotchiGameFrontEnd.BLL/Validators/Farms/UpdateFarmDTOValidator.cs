using FluentValidation;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Farm;

namespace InnoGotchiGameFrontEnd.BLL.Validators.Farms
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

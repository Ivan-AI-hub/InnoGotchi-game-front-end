using FluentValidation;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Farm;

namespace InnoGotchiGameFrontEnd.BLL.Validators.Farms
{
    internal class AddFarmDTOValidator : AbstractValidator<AddFarmDTOModel>
    {
        public AddFarmDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}

using FluentValidation;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Pet;

namespace InnoGotchiGameFrontEnd.BLL.Validators.Pets
{
    internal class UpdatePetDTOValidator :AbstractValidator<UpdatePetDTOModel>
    {
        public UpdatePetDTOValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.UpdatedId).NotNull().Must(x => x > 0);
        }
    }
}

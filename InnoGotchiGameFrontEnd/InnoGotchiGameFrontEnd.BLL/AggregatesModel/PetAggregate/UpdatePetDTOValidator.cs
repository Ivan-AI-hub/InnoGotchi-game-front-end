using FluentValidation;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate
{
    internal class UpdatePetDTOValidator : AbstractValidator<UpdatePetDTOModel>
    {
        public UpdatePetDTOValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.UpdatedId).NotNull().Must(x => x > 0);
        }
    }
}

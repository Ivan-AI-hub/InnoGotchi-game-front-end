using FluentValidation;


namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate
{
    internal class AddPetDTOValidator : AbstractValidator<AddPetDTOModel>
    {
        public AddPetDTOValidator()
        {
            RuleFor(x => x.FarmId).NotNull().Must(x => x > 0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.View).NotNull();
        }
    }
}

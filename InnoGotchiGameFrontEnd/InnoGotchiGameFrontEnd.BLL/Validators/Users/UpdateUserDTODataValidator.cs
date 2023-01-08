using FluentValidation;
using InnoGotchiGameFrontEnd.BLL.ComandModels.User;

namespace InnoGotchiGameFrontEnd.BLL.Validators.Users
{
    internal class UpdateUserDTODataValidator : AbstractValidator<UpdateUserDTODataModel>
    {
        public UpdateUserDTODataValidator()
        {
            RuleFor(x => x.UpdatedId)
                .NotNull()
                .Must(x => x > 0);
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull()
                .Length(4, 20);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull()
                .Length(4, 20);
        }
    }
}

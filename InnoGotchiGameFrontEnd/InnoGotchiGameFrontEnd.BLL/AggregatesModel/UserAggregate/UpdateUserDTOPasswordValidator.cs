using FluentValidation;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.UserAggregate
{
    internal class UpdateUserDTOPasswordValidator : AbstractValidator<UpdateUserDTOPasswordModel>
    {
        public UpdateUserDTOPasswordValidator()
        {
            RuleFor(x => x.UpdatedId)
                .NotNull()
                .Must(x => x > 0);

            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .Matches(x => x.ConfirmPassword).WithMessage("Пароли не совпадают");
        }
    }
}

﻿using FluentValidation;
using InnoGotchiGameFrontEnd.BLL.ComandModels.User;

namespace InnoGotchiGameFrontEnd.BLL.Validators.Users
{
    internal class AddUserDTOValidator : AbstractValidator<AddUserDTOModel>
    {
        public AddUserDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .Matches("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull()
                .Length(4, 20);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull()
                .Length(4, 20);

            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(x => x.RePassword).WithMessage("Пароли не совпадают");
        }
    }
}

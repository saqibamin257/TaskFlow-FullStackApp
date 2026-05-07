using FluentValidation;

using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;

namespace TaskFlow.Modules.Users.Application.Features.CreateUser
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationKeys.NameRequired)
                .MaximumLength(200);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ValidationKeys.EmailRequired)
                .EmailAddress()
                .WithMessage(ValidationKeys.InvalidEmail);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(ValidationKeys.PasswordRequired)
                .MinimumLength(6);

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage(ValidationKeys.RoleRequired);

            RuleFor(x => x.TenantId)
                .NotEmpty()
                .WithMessage(ValidationKeys.TenantRequired);
        }
    }
}

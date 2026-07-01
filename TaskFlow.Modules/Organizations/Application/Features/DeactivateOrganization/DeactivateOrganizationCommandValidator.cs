using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;

namespace TaskFlow.Modules.Organizations.Application.Features.DeactivateOrganization
{
    public sealed class DeactivateOrganizationCommandValidator: AbstractValidator<DeactivateOrganizationCommand>
    {
        public DeactivateOrganizationCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationKeys.IdRequired);
        }
    }
}

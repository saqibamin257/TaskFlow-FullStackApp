using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;

namespace TaskFlow.Modules.Organizations.Application.Features.UpdateOrganization
{
    public sealed class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
    {
        public UpdateOrganizationCommandValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ValidationKeys.OrganizationNameRequired)
            .MaximumLength(200);

            RuleFor(x => x.Slug)
                .NotEmpty()
                 .WithMessage(ValidationKeys.OrganizationSlugRequired)
                 .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.LogoUrl)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.LogoUrl));
        }
    }
}

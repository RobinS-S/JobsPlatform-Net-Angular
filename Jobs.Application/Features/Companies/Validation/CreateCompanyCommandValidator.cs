using FluentValidation;
using Jobs.Application.Features.Companies.Dto;
using Jobs.Application.Features.Companies.Validation;

namespace Jobs.Application.Features.Companies.Validation
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyRequest>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Company name is required")
                .MaximumLength(256).WithMessage("Company name must not exceed 256 characters");

            RuleFor(x => x.Website)
                .MaximumLength(2048).WithMessage("Website URL must not exceed 2048 characters")
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Website must be a valid URL");

            RuleFor(x => x.Location)
                .NotNull().WithMessage("Location is required")
                .SetValidator(new LocationValidator());
        }
    }
}

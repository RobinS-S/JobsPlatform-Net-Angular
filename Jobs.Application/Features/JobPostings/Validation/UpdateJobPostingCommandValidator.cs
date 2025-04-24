using FluentValidation;
using Jobs.Application.Features.JobPostings.Dto;

namespace Jobs.Application.Features.JobPostings.Validation
{
    public class UpdateJobPostingCommandValidator : AbstractValidator<UpdateJobPostingRequest>
    {
        public UpdateJobPostingCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Job title is required")
                .MaximumLength(256).WithMessage("Job title must not exceed 256 characters");

            RuleFor(x => x.Description)
                .MaximumLength(4096).WithMessage("Description must not exceed 4096 characters");

            RuleFor(x => x.ContactEmail)
                .EmailAddress().WithMessage("Contact email must be a valid email address")
                .MaximumLength(320).WithMessage("Contact email must not exceed 320 characters")
                .When(x => !string.IsNullOrEmpty(x.ContactEmail));

            RuleFor(x => x.JobUrl)
                .MaximumLength(2048).WithMessage("Job URL must not exceed 2048 characters")
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Job URL must be a valid URL");

            RuleFor(x => x.MinHoursPerWeek)
                .GreaterThanOrEqualTo(1).WithMessage("Minimum hours per week must be at least 1")
                .LessThanOrEqualTo(168).WithMessage("Minimum hours per week cannot exceed 168")
                .When(x => x.MinHoursPerWeek.HasValue);

            RuleFor(x => x.MaxHoursPerWeek)
                .GreaterThanOrEqualTo(1).WithMessage("Maximum hours per week must be at least 1")
                .LessThanOrEqualTo(168).WithMessage("Maximum hours per week cannot exceed 168")
                .When(x => x.MaxHoursPerWeek.HasValue);

            RuleFor(x => x)
                .Must(x => !x.MinHoursPerWeek.HasValue || !x.MaxHoursPerWeek.HasValue || x.MinHoursPerWeek <= x.MaxHoursPerWeek)
                .WithMessage("Minimum hours per week cannot be greater than maximum hours per week")
                .When(x => x.MinHoursPerWeek.HasValue && x.MaxHoursPerWeek.HasValue);
        }
    }
}

using FluentValidation;
using Jobs.Application.Common;

namespace Jobs.Application.Pagination
{
    public class FilteringOptionsValidator : AbstractValidator<FilteringOptions>
    {
        public FilteringOptionsValidator()
        {
            RuleFor(x => x.SearchText)
                .MaximumLength(100)
                .WithMessage("Search text cannot exceed 100 characters");
        }
    }
}
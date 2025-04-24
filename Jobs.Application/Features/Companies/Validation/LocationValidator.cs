using FluentValidation;
using Jobs.Domain.ValueObjects;
using Nager.Country;

namespace Jobs.Application.Features.Companies.Validation
{
    public class LocationValidator : AbstractValidator<Location>
    {
        private readonly CountryProvider _countryProvider;

        public LocationValidator()
        {
            _countryProvider = new CountryProvider();

            RuleFor(x => x.AddressLines)
                .NotEmpty().WithMessage("At least one address line is required")
                .Must(lines => lines.Count <= 4).WithMessage("Maximum 4 address lines are allowed");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required");

            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage("Country code is required")
                .Must(IsValidCountry)
                .WithMessage("Country code must be a valid ISO 3166-1 alpha-2 code (e.g., 'nl', 'be', 'fr')");

            When(x => x.GeoLocation != null, () =>
            {
                RuleFor(x => x.GeoLocation!.Value.Latitude)
                    .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90");

                RuleFor(x => x.GeoLocation!.Value.Longitude)
                    .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180");
            });
        }

        private bool IsValidCountry(string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                return false;
            }

            try
            {
                return _countryProvider.GetCountry(countryCode) != null;
            }
            catch
            {
                return false;
            }
        }
    }
}

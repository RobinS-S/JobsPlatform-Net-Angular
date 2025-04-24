using Bogus;
using Jobs.Domain.Common;
using Jobs.Domain.Entities;
using Jobs.Domain.ValueObjects;
using Jobs.Infrastructure.Data.Seeders.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jobs.Infrastructure.Data.Seeders
{
    public class CompanySeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CompanySeeder> _logger;

        public CompanySeeder(ApplicationDbContext context, ILogger<CompanySeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            if (await _context.Companies.AnyAsync())
            {
                return;
            }

            _logger.LogInformation("Seeding database with sample companies...");

            var genericFaker = new Faker();

            var countries = CompanySeederConstants.Countries;
            var countryLocales = CompanySeederConstants.CountryLocales;

            var companyCount = genericFaker.Random.Int(30, 100);

            var companyGenerator = new Faker<Company>()
                .CustomInstantiator(f =>
                {
                    var countryCode = f.PickRandom(countries);
                    var isDutch = countryCode == "nl";
                    var locale = countryLocales.TryGetValue(countryCode, out string? value) ? value : "en";
                    var countryFaker = new Faker(locale);

                    var location = new Location
                    {
                        CountryCode = countryCode,
                        City = countryFaker.Address.City(),
                        StateProvince = countryFaker.Address.State(),
                        PostalCode = countryFaker.Address.ZipCode(),
                    };

                    var addressLines = new List<string>
                    {
                        countryFaker.Address.StreetAddress(),
                    };

                    if (isDutch && countryFaker.Random.Bool(0.3f))
                    {
                        addressLines.Add(countryFaker.PickRandom(CompanySeederConstants.ExtraDutchAddressLines));
                    }

                    if (countryFaker.Random.Bool(0.3f))
                    {
                        addressLines.Add(countryFaker.Address.SecondaryAddress());
                    }

                    location.AddressLines = addressLines;

                    if (countryFaker.Random.Bool(0.7f))
                    {
                        location.GeoLocation = new GeoCoordinates(
                            countryFaker.Random.Double(CompanySeederConstants.MinLatitude, CompanySeederConstants.MaxLatitude),
                            countryFaker.Random.Double(CompanySeederConstants.MinLongitude, CompanySeederConstants.MaxLongitude));
                    }

                    return new Company
                    {
                        Name = countryFaker.Company.CompanyName(),
                        Website = countryFaker.Random.Bool(0.8f) ? countryFaker.Internet.Url().ToLower() : null,
                        Location = location,
                    };
                });

            var companies = companyGenerator.Generate(companyCount);
            await _context.Companies.AddRangeAsync(companies);

            _logger.LogInformation("Added {CompanyCount} sample companies to the database", companyCount);
        }
    }
}

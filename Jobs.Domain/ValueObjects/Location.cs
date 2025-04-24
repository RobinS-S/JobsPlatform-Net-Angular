using Jobs.Domain.Common;

namespace Jobs.Domain.ValueObjects
{
    public class Location
    {
        public List<string> AddressLines { get; set; } = [];

        public string City { get; set; } = string.Empty;

        public string? StateProvince { get; set; }

        public string? PostalCode { get; set; } // Some countries don't require postal codes, for now I've made it optional

        public string CountryCode { get; set; } = string.Empty;

        public GeoCoordinates? GeoLocation { get; set; }
    }
}

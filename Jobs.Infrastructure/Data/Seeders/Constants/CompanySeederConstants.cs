namespace Jobs.Infrastructure.Data.Seeders.Constants
{
    public static class CompanySeederConstants
    {
        public const double MinLatitude = 50.7;
        public const double MaxLatitude = 53.5;
        public const double MinLongitude = 3.3;
        public const double MaxLongitude = 7.2;

        public static readonly string[] ExtraDutchAddressLines = [
            "1e Verdieping", "2e Verdieping", "Unit B", "Gebouw C", "Kantoor 2.4"
        ];

        public static readonly string[] Countries = [
            "nl", "be", "de", "fr", "gb", "us", "es", "it", "se", "dk"
        ];

        // Some countries fall back to english as bogus doesn't have every locale
        public static readonly Dictionary<string, string> CountryLocales = new()
        {
            { "nl", "nl" },
            { "be", "fr" },
            { "de", "de" },
            { "fr", "fr" },
            { "gb", "en_GB" },
            { "us", "en" },
            { "es", "es" },
            { "it", "it" },
            { "se", "sv" },
            { "dk", "en_GB" },
        };
    }
}

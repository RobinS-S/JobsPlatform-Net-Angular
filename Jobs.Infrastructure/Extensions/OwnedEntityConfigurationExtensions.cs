using Jobs.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobs.Infrastructure.Extensions
{
    public static class OwnedEntityConfigurationExtensions
    {
        public static void ConfigureLocation<TOwner>(
            this OwnedNavigationBuilder<TOwner, Location> builder)
            where TOwner : class
        {
            builder.Property(l => l.City)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(l => l.StateProvince)
                .HasMaxLength(128);

            builder.Property(l => l.PostalCode)
                .HasMaxLength(20);

            builder.Property(l => l.CountryCode)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false); // These only exist out of two letters

            builder.Property(l => l.GeoLocation)
                .HasPointConversion(); // Using the extension method for GeoCoordinates

            // Because the address lines are not very long and there are only a maximum of 4, I feel safe to use JSON in DB.
            // There are cases where I would do this relationally for performance and database querying purposes, but this saves time.
            builder.Property(l => l.AddressLines)
                .HasMaxLength(4 * 128);
        }
    }
}

using Jobs.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;

namespace Jobs.Infrastructure.Extensions
{
    public static class GeoCoordinatesConfigurationExtensions
    {
        public static PropertyBuilder<GeoCoordinates> HasPointConversion(this PropertyBuilder<GeoCoordinates> builder, int srid = 4326)
        {
            return builder
                .HasConversion(
                    coords => CreatePoint(coords.Longitude, coords.Latitude, srid),
                    point => CreateGeoCoordinates(point))
                .HasColumnType("POINT")
                .HasSrid(srid);
        }

        public static PropertyBuilder<GeoCoordinates?> HasPointConversion(this PropertyBuilder<GeoCoordinates?> builder, int srid = 4326)
        {
            return builder
                .HasConversion(
                    coords => coords.HasValue ? CreatePoint(coords.Value.Longitude, coords.Value.Latitude, srid) : null,
                    point => point != null ? CreateGeoCoordinates(point) : null)
                .HasColumnType("POINT")
                .HasSrid(srid);
        }

        private static Point CreatePoint(double longitude, double latitude, int srid) => new(longitude, latitude) { SRID = srid };

        private static GeoCoordinates CreateGeoCoordinates(Point point) => new(point.Y, point.X);
    }
}

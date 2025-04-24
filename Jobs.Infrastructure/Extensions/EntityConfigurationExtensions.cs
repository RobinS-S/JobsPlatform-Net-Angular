using Jobs.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobs.Infrastructure.Extensions
{
    public static class EntityConfigurationExtensions
    {
        public static EntityTypeBuilder<T> ConfigureEntityId<T>(
            this EntityTypeBuilder<T> builder)
            where T : class, IEntityBase
        {
            builder.HasKey(c => c.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            return builder;
        }
    }
}

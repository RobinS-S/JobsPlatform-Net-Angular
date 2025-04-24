using Ardalis.Specification;
using Jobs.Application.Pagination;
using Jobs.Domain.Common.Interfaces;

namespace Jobs.Application.Extensions
{
    public static class PaginationExtensions
    {
        public static ISpecificationBuilder<T> Page<T>(
          this ISpecificationBuilder<T> builder,
          PaginationParameters? parameters)
          where T : IEntityBase
        {
            if (parameters == null)
            {
                return builder;
            }

            var skipAmount = (parameters.PageNumber - 1) * parameters.PageSize;

            builder.Skip(skipAmount);
            builder.Take(parameters.PageSize);

            return builder;
        }
    }
}

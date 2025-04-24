using Ardalis.Specification;
using Jobs.Application.Common;
using Jobs.Application.Extensions;
using Jobs.Application.Features.Companies.Filters;
using Jobs.Application.Pagination;
using Jobs.Domain.Entities;

namespace Jobs.Application.Features.Companies.Specifications
{
    public sealed class CompanySpecification : Specification<Company>
    {
        public CompanySpecification(
            PaginationParameters? paginationParameters = null,
            FilteringOptions? filteringOptions = null,
            CompanyFilter? companyFilter = null,
            bool track = true)
        {
            Query.OrderBy(c => c.Name);

            Query.Page(paginationParameters);

            if (companyFilter?.MinActiveJobs > 0)
            {
                Query.Where(c => c.JobPostings.Count >= companyFilter.MinActiveJobs);
            }

            if (!string.IsNullOrEmpty(filteringOptions?.SearchText))
            {
                Query.Search(c => c.Name, $"%{filteringOptions.SearchText}%");
            }

            if (!track)
            {
                Query.AsNoTracking();
            }
        }
    }
}
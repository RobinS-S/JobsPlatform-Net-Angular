using Ardalis.Specification;
using Jobs.Application.Common;
using Jobs.Application.Extensions;
using Jobs.Application.Pagination;
using Jobs.Domain.Entities;
using Jobs.Domain.Enums;

namespace Jobs.Application.Features.JobPostings.Specifications
{
    public sealed class JobPostingSpecification : Specification<JobPosting>
    {
        public JobPostingSpecification(
            PaginationParameters? paginationParameters = null,
            FilteringOptions? filteringOptions = null,
            Guid? companyId = null,
            JobCategory? category = null,
            bool includeCompany = true,
            bool track = true)
        {
            Query.Page(paginationParameters);

            if (companyId.HasValue)
            {
                Query.Where(j => j.CompanyId == companyId.Value);
            }

            if (category.HasValue)
            {
                Query.Where(j => j.Category == category.Value);
            }

            if (includeCompany)
            {
                Query.Include(j => j.Company);
            }

            if (!string.IsNullOrEmpty(filteringOptions?.SearchText))
            {
                Query.Search(c => c.Title, $"%{filteringOptions.SearchText}%");
            }

            Query.OrderByDescending(j => j.CreatedAt);

            if (!track)
            {
                Query.AsNoTracking();
            }
        }
    }
}

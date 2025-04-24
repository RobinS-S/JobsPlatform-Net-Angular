using Ardalis.Specification;
using Jobs.Domain.Entities;

namespace Jobs.Application.Features.JobPostings.Specifications
{
    public sealed class JobPostingByIdSpecification : SingleResultSpecification<JobPosting>
    {
        public JobPostingByIdSpecification(Guid id)
        {
            Query.Where(j => j.Id == id)
                .Include(j => j.Company);
        }
    }
}

using Jobs.Application.Common;
using Jobs.Application.Features.JobPostings.Dto;
using Jobs.Application.Pagination;
using Jobs.Domain.Enums;

namespace Jobs.Application.Features.JobPostings
{
    public interface IJobPostingService
    {
        Task<JobPostingResponse> CreateJobPostingAsync(CreateJobPostingRequest createJobPosting, CancellationToken cancellationToken = default);

        Task<JobPostingResponse> UpdateJobPostingAsync(Guid id, UpdateJobPostingRequest updateJobPosting, CancellationToken cancellationToken = default);

        Task<bool> DeleteJobPostingAsync(Guid id, CancellationToken cancellationToken = default);

        Task<JobPostingResponse?> GetJobPostingByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PaginatedListResult<JobPostingResponse>> GetJobPostingsPaginatedAsync(
            PaginationParameters pagination,
            FilteringOptions filteringOptions,
            Guid? companyId = null,
            JobCategory? category = null,
            CancellationToken cancellationToken = default);
    }
}

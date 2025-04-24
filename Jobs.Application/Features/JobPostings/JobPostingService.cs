using Ardalis.Specification;
using Jobs.Application.Common;
using Jobs.Application.Exceptions;
using Jobs.Application.Features.JobPostings.Dto;
using Jobs.Application.Features.JobPostings.Specifications;
using Jobs.Application.Pagination;
using Jobs.Domain.Entities;
using Jobs.Domain.Enums;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace Jobs.Application.Features.JobPostings
{
    public sealed class JobPostingService : IJobPostingService
    {
        private readonly IRepositoryBase<JobPosting> _repository;
        private readonly IRepositoryBase<Company> _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<JobPostingService> _logger;

        public JobPostingService(
            IRepositoryBase<JobPosting> repository,
            IRepositoryBase<Company> companyRepository,
            IMapper mapper,
            ILogger<JobPostingService> logger)
        {
            _repository = repository;
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<JobPostingResponse> CreateJobPostingAsync(CreateJobPostingRequest createJobPosting, CancellationToken cancellationToken = default)
        {
            var company = await _companyRepository.GetByIdAsync(createJobPosting.CompanyId, cancellationToken)
                ?? throw new NotFoundException($"Company with ID {createJobPosting.CompanyId} not found.");

            var jobPosting = _mapper.Map<JobPosting>(createJobPosting);

            await _repository.AddAsync(jobPosting, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Job posting {Title} created for company {CompanyName}.", jobPosting.Title, company.Name);

            jobPosting.Company = company;

            return _mapper.Map<JobPostingResponse>(jobPosting);
        }

        public async Task<JobPostingResponse> UpdateJobPostingAsync(Guid id, UpdateJobPostingRequest updateJobPosting, CancellationToken cancellationToken = default)
        {
            var jobPosting = await _repository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException($"Job posting with ID {id} not found.");

            _mapper.Map(updateJobPosting, jobPosting);
            jobPosting.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(jobPosting, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Job posting {Title} updated.", jobPosting.Title);

            var specification = new JobPostingByIdSpecification(id);
            var updatedJobPosting = await _repository.SingleOrDefaultAsync(specification, cancellationToken)
                ?? throw new NotFoundException();

            return _mapper.Map<JobPostingResponse>(updatedJobPosting);
        }

        public async Task<bool> DeleteJobPostingAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var jobPosting = await _repository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException($"Job posting with ID {id} not found.");

            await _repository.DeleteAsync(jobPosting, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Job posting {Title} deleted.", jobPosting.Title);

            return true;
        }

        public async Task<JobPostingResponse?> GetJobPostingByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var specification = new JobPostingByIdSpecification(id);
            var jobPosting = await _repository.SingleOrDefaultAsync(specification, cancellationToken)
                ?? throw new NotFoundException($"Job posting with ID {id} not found.");

            return _mapper.Map<JobPostingResponse>(jobPosting);
        }

        public async Task<PaginatedListResult<JobPostingResponse>> GetJobPostingsPaginatedAsync(
            PaginationParameters pagination,
            FilteringOptions filteringOptions,
            Guid? companyId = null,
            JobCategory? category = null,
            CancellationToken cancellationToken = default)
        {
            var specification = new JobPostingSpecification(pagination, filteringOptions, companyId, category, includeCompany: true, track: false);

            var totalCount = await _repository.CountAsync(specification, cancellationToken);
            var jobPostings = await _repository.ListAsync(specification, cancellationToken);

            var jobPostingDtos = _mapper.Map<List<JobPostingResponse>>(jobPostings);

            return new PaginatedListResult<JobPostingResponse>
            {
                Items = jobPostingDtos,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalPages = (totalCount + pagination.PageSize - 1) / pagination.PageSize,
            };
        }
    }
}

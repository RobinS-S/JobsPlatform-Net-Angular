using Ardalis.Specification;
using Jobs.Application.Common;
using Jobs.Application.Exceptions;
using Jobs.Application.Features.Companies.Dto;
using Jobs.Application.Features.Companies.Filters;
using Jobs.Application.Features.Companies.Specifications;
using Jobs.Application.Pagination;
using Jobs.Domain.Entities;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace Jobs.Application.Features.Companies
{
    public sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryBase<Company> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(IRepositoryBase<Company> repository, IMapper mapper, ILogger<CompanyService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CompanyResponse> CreateCompanyAsync(CreateCompanyRequest createCompany, CancellationToken cancellationToken = default)
        {
            var company = _mapper.Map<Company>(createCompany);

            await _repository.AddAsync(company, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Company {Name} created.", company.Name);

            return _mapper.Map<CompanyResponse>(company);
        }

        public async Task<CompanyResponse> UpdateCompanyAsync(Guid id, UpdateCompanyRequest updateCompany, CancellationToken cancellationToken = default)
        {
            var company = await _repository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException();

            _mapper.Map(updateCompany, company);

            await _repository.UpdateAsync(company, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Company {Name} updated.", company.Name);

            return _mapper.Map<CompanyResponse>(company);
        }

        public async Task<bool> DeleteCompanyAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var company = await _repository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException();

            await _repository.DeleteAsync(company, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Company {Name} deleted.", company.Name);

            return true;
        }

        public async Task<CompanyResponse?> GetCompanyByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var specification = new CompanyByIdSpecification(id);
            var company = await _repository.SingleOrDefaultAsync(specification, cancellationToken)
                ?? throw new NotFoundException();

            return _mapper.Map<CompanyResponse>(company);
        }

        public async Task<PaginatedListResult<CompanyResponse>> GetCompaniesPaginatedAsync(PaginationParameters pagination, FilteringOptions filteringOptions, CompanyFilter companyFilter, CancellationToken cancellationToken = default)
        {
            var specification = new CompanySpecification(pagination, filteringOptions, companyFilter, track: false);

            var totalCount = await _repository.CountAsync(specification, cancellationToken);
            var companies = await _repository.ListAsync(specification, cancellationToken);

            var companyDtos = _mapper.Map<List<CompanyResponse>>(companies);

            return new PaginatedListResult<CompanyResponse>
            {
                Items = companyDtos,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalPages = (totalCount + pagination.PageSize - 1) / pagination.PageSize,
            };
        }
    }
}

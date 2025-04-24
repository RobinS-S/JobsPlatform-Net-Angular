using Jobs.Application.Common;
using Jobs.Application.Features.Companies.Dto;
using Jobs.Application.Features.Companies.Filters;
using Jobs.Application.Pagination;

namespace Jobs.Application.Features.Companies
{
    public interface ICompanyService
    {
        Task<CompanyResponse> CreateCompanyAsync(CreateCompanyRequest createCompany, CancellationToken cancellationToken = default);

        Task<CompanyResponse> UpdateCompanyAsync(Guid id, UpdateCompanyRequest updateCompany, CancellationToken cancellationToken = default);

        Task<bool> DeleteCompanyAsync(Guid id, CancellationToken cancellationToken = default);

        Task<CompanyResponse?> GetCompanyByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PaginatedListResult<CompanyResponse>> GetCompaniesPaginatedAsync(PaginationParameters pagination, FilteringOptions filteringOptions, CompanyFilter companyFilter, CancellationToken cancellationToken = default);
    }
}

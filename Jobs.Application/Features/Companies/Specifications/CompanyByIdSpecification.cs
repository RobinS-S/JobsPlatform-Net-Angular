using Ardalis.Specification;
using Jobs.Domain.Entities;

namespace Jobs.Application.Features.Companies.Specifications
{
    public sealed class CompanyByIdSpecification : SingleResultSpecification<Company>
    {
        public CompanyByIdSpecification(Guid id)
        {
            Query.Where(c => c.Id == id);
        }
    }
}
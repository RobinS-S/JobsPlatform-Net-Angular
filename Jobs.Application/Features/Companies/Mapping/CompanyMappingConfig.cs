using Jobs.Application.Features.Companies.Dto;
using Jobs.Domain.Entities;
using Mapster;

namespace Jobs.Application.Features.Companies.Mapping
{
    public class CompanyMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Company, CompanyResponse>();

            config.NewConfig<CreateCompanyRequest, Company>();

            config.NewConfig<UpdateCompanyRequest, Company>();
        }
    }
}

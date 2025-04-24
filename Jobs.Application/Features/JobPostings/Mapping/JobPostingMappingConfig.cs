using Jobs.Application.Features.JobPostings.Dto;
using Jobs.Domain.Entities;
using Mapster;

namespace Jobs.Application.Features.JobPostings.Mapping
{
    public class JobPostingMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<JobPosting, JobPostingResponse>()
                .Map(dest => dest.CompanyName, src => src.Company.Name);

            config.NewConfig<CreateJobPostingRequest, JobPosting>();

            config.NewConfig<UpdateJobPostingRequest, JobPosting>();
        }
    }
}

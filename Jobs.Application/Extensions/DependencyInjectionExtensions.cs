using FluentValidation;
using Jobs.Application.Common.Interfaces;
using Jobs.Application.Features.Companies;
using Jobs.Application.Features.JobPostings;
using Jobs.Application.Pagination;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Jobs.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddMapster();

            var config = new TypeAdapterConfig();
            config.Scan(typeof(IApplicationDbContext).Assembly);

            builder.Services.AddSingleton(config);
            builder.Services.AddScoped<IMapper, ServiceMapper>();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<PaginationParametersValidator>();

            // Application services
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IJobPostingService, JobPostingService>();
        }
    }
}

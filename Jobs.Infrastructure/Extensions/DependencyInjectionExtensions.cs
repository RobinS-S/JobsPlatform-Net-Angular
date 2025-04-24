using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Jobs.Application.Common.Interfaces;
using Jobs.Infrastructure;
using Jobs.Infrastructure.Data;
using Jobs.Infrastructure.Data.Interceptors;
using Jobs.Infrastructure.Data.Repositories;
using Jobs.Infrastructure.Data.Seeders;
using Jobs.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jobs.Infrastructure.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString(Constants.SqliteConnectionString)
                ?? throw new Exception($"{nameof(Constants.SqliteConnectionString)} connection string is not configured.");

            builder.Services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                options.UseSqlite(connectionString, x => x.UseNetTopologySuite());
                options.AddInterceptors(new TrackedEntityUpdateInterceptor(TimeProvider.System));

                options.AddDatabaseSeeding(provider);
            });

            builder.Services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
            builder.Services.AddScoped<ApplicationDbContextInitializer>();

            builder.Services.AddScoped<CompanySeeder>();
            builder.Services.AddScoped<JobPostingSeeder>();

            builder.Services
                .AddScoped(typeof(IRepositoryBase<>), typeof(EfCoreRepository<>))
                .AddScoped<ISpecificationEvaluator, SpecificationEvaluator>();
        }
    }
}

using Jobs.API;
using Jobs.Infrastructure.Data;

namespace Jobs.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApiServices(this IHostApplicationBuilder builder)
        {
            var corsAllowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]?>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(Constants.CorsPolicyName, policy =>
                {
                    if (corsAllowedOrigins != null)
                    {
                        policy.WithOrigins(corsAllowedOrigins)
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    }
                });
            });

            builder.Services.AddExceptionHandler<ApiExceptionHandler>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            builder.Services.AddEndpointsApiExplorer();
        }
    }
}

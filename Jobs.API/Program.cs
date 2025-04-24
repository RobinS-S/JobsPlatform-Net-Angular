using Jobs.API.Extensions;
using Jobs.Application.Extensions;
using Jobs.Infrastructure.Extensions;

namespace Jobs.API
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Core API, versioning, controllers, Swagger, etc
            builder.Services.AddApiCoreServices();
            builder.AddApplicationServices();
            builder.AddInfrastructureServices();
            builder.AddApiServices();

            var app = builder.Build();

            await app.UseApiCorePipeline();
            await app.RunAsync();
        }
    }
}

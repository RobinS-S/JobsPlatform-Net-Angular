using Jobs.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Jobs.Infrastructure.Extensions
{
    public static class DbSeedingExtensions
    {
        public static void AddDatabaseSeeding(this DbContextOptionsBuilder builder, IServiceProvider serviceProvider)
        {
            builder.UseAsyncSeeding(async (_, _, _) =>
            {
                var initializer = serviceProvider.GetRequiredService<ApplicationDbContextInitializer>();

                await initializer.SeedAsync();
            });
        }

        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

            await initializer.InitializeAsync();
        }
    }
}

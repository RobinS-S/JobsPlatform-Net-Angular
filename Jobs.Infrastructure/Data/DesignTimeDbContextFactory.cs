using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Jobs.Infrastructure.Data
{
    public sealed class DesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var cfg = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var cs = cfg.GetConnectionString(Constants.SqliteConnectionString)
                     ?? throw new InvalidOperationException("Missing connection string.");

            var opts = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(cs, x => x.UseNetTopologySuite())
                .Options;

            return new ApplicationDbContext(opts);
        }
    }
}

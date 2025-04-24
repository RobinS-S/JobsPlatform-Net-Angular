using Jobs.Infrastructure.Data.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jobs.Infrastructure.Data
{
    // Inspired from example project. Initializer seems much simpler than the factory pattern sometimes used for seeding.
    public class ApplicationDbContextInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApplicationDbContextInitializer> _logger;
        private readonly CompanySeeder _companySeeder;
        private readonly JobPostingSeeder _jobPostingSeeder;

        public ApplicationDbContextInitializer(
            ApplicationDbContext context,
            ILogger<ApplicationDbContextInitializer> logger,
            CompanySeeder companySeeder,
            JobPostingSeeder jobPostingSeeder)
        {
            _context = context;
            _logger = logger;
            _companySeeder = companySeeder;
            _jobPostingSeeder = jobPostingSeeder;
        }

        public async Task InitializeAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            await _companySeeder.SeedAsync();
            await _context.SaveChangesAsync();

            await _jobPostingSeeder.SeedAsync();
            await _context.SaveChangesAsync();
        }
    }
}

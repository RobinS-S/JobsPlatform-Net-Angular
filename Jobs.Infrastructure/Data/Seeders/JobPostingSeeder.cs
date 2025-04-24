using Bogus;
using Jobs.Domain.Entities;
using Jobs.Domain.Enums;
using Jobs.Infrastructure.Data.Seeders.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jobs.Infrastructure.Data.Seeders
{
    public class JobPostingSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<JobPostingSeeder> _logger;

        public JobPostingSeeder(ApplicationDbContext context, ILogger<JobPostingSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            if (await _context.JobPostings.AnyAsync())
            {
                return;
            }

            var companies = await _context.Companies.ToListAsync();
            if (!companies.Any())
            {
                _logger.LogWarning("No companies found for job posting seeding");
                return;
            }

            _logger.LogInformation("Seeding database with sample job postings...");

            var faker = new Faker();
            var jobPostings = new List<JobPosting>();

            foreach (var company in companies)
            {
                var jobCount = faker.Random.Int(0, 4);

                for (var i = 0; i < jobCount; i++)
                {
                    var category = faker.PickRandom<JobCategory>();
                    var title = faker.PickRandom(JobPostingSeederConstants.JobTitlesByCategory[category]);

                    if (faker.Random.Bool(0.4f))
                    {
                        if (faker.Random.Bool())
                        {
                            title += $" - {faker.Commerce.ProductAdjective()}";
                        }
                        else
                        {
                            title += $" ({company.Location.City})";
                        }
                    }

                    var website = company.Website?.Replace("http://", string.Empty).Replace("https://", string.Empty).Replace("www.", string.Empty);
                    var jobPosting = new JobPosting
                    {
                        Title = title,
                        Company = company,
                        CompanyId = company.Id,
                        Category = category,
                        CreatedAt = faker.Date.Between(DateTime.UtcNow.AddMonths(-6), DateTime.UtcNow),
                        Description = faker.Random.Bool(0.8f) ?
                            GenerateJobDescription(faker, title, category) : null,
                        AllowsRemoteWork = faker.Random.Bool(0.7f) ? faker.Random.Bool() : null,
                        ContactEmail = faker.Random.Bool(0.7f) ?
                            faker.Internet.Email(provider: website) : null,
                        JobUrl = faker.Random.Bool(0.6f) && !string.IsNullOrEmpty(company.Website) ?
                            $"{company.Website?.TrimEnd('/')}/careers/job-{faker.Random.AlphaNumeric(6).ToLower()}" : null,
                    };

                    if (faker.Random.Bool(0.7f))
                    {
                        var hoursPattern = faker.PickRandom(JobPostingSeederConstants.WorkingHoursPatterns);
                        jobPosting.MinHoursPerWeek = hoursPattern.MinHours;
                        jobPosting.MaxHoursPerWeek = hoursPattern.MaxHours;
                    }

                    jobPostings.Add(jobPosting);
                }
            }

            await _context.JobPostings.AddRangeAsync(jobPostings);

            _logger.LogInformation("Added {jobPostingCount} sample job postings to the database", jobPostings.Count);
        }

        private string GenerateJobDescription(Faker faker, string title, JobCategory category)
        {
            var sections = new List<string>
            {
                string.Format(faker.PickRandom(JobPostingSeederConstants.JobIntroductions), title),
            };

            if (faker.Random.Bool(0.6f))
            {
                sections.Add(faker.PickRandom(JobPostingSeederConstants.CompanyDescriptions));
            }

            var responsibilities = new List<string>();
            var respCount = faker.Random.Int(3, 6);

            var categoryResponsibilities = new List<string>(JobPostingSeederConstants.CategoryResponsibilities[category]);

            for (var i = 0; i < respCount; i++)
            {
                if (categoryResponsibilities.Count > 0 && i < 3)
                {
                    responsibilities.Add(faker.PickRandom(categoryResponsibilities));

                    categoryResponsibilities.Remove(responsibilities.Last());
                }
                else
                {
                    responsibilities.Add(faker.PickRandom(JobPostingSeederConstants.GenericResponsibilities));
                }
            }

            sections.Add("Responsibilities:\n• " + string.Join("\n• ", responsibilities));

            var requirements = new List<string>();
            var reqCount = faker.Random.Int(3, 5);

            var categoryRequirements = new List<string>(JobPostingSeederConstants.CategoryRequirements[category]);

            for (var i = 0; i < reqCount; i++)
            {
                if (categoryRequirements.Count > 0 && i < 3)
                {
                    requirements.Add(faker.PickRandom(categoryRequirements));

                    categoryRequirements.Remove(requirements.Last());
                }
                else
                {
                    requirements.Add(faker.PickRandom(JobPostingSeederConstants.GenericRequirements));
                }
            }

            sections.Add("Requirements:\n• " + string.Join("\n• ", requirements));

            if (faker.Random.Bool(0.7f))
            {
                var benefits = new List<string>();
                var benefitCount = faker.Random.Int(2, 5);

                for (var i = 0; i < benefitCount; i++)
                {
                    benefits.Add(faker.PickRandom(JobPostingSeederConstants.Benefits));
                }

                sections.Add("We offer:\n• " + string.Join("\n• ", benefits));
            }

            if (faker.Random.Bool(0.8f))
            {
                sections.Add(faker.PickRandom(JobPostingSeederConstants.Conclusions));
            }

            return string.Join("\n\n", sections);
        }
    }
}

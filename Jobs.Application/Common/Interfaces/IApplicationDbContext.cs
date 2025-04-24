using Jobs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Company> Companies { get; set; }

        DbSet<JobPosting> JobPostings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

using Jobs.Domain.Common.Interfaces;
using Jobs.Domain.ValueObjects;

namespace Jobs.Domain.Entities
{
    public class Company : IEntityBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public Location Location { get; set; } = null!;

        public string? Website { get; set; }

        public ICollection<JobPosting> JobPostings { get; set; } = [];
    }
}

using Jobs.Domain.Common.Interfaces;
using Jobs.Domain.Enums;

namespace Jobs.Domain.Entities
{
    public class JobPosting : ITrackedEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public JobCategory? Category { get; set; }

        public int? MinHoursPerWeek { get; set; }

        public int? MaxHoursPerWeek { get; set; }

        public bool? AllowsRemoteWork { get; set; }

        public string? ContactEmail { get; set; }

        public string? JobUrl { get; set; }

        public Guid CompanyId { get; set; }

        public Company Company { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

using Jobs.Domain.Enums;

namespace Jobs.Application.Features.JobPostings.Dto
{
    /// <summary>
    /// Response DTO for a job posting.
    /// </summary>
    public class JobPostingResponse
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

        public string CompanyName { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

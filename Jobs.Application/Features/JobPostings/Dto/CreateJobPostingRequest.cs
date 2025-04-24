using Jobs.Domain.Enums;

namespace Jobs.Application.Features.JobPostings.Dto
{
    /// <summary>
    /// Request DTO for creating a job posting.
    /// </summary>
    public class CreateJobPostingRequest
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public JobCategory? Category { get; set; }

        public int? MinHoursPerWeek { get; set; }

        public int? MaxHoursPerWeek { get; set; }

        public bool? AllowsRemoteWork { get; set; }

        public string? ContactEmail { get; set; }

        public string? JobUrl { get; set; }

        public Guid CompanyId { get; set; }
    }
}

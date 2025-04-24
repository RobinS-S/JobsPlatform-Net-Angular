using Jobs.Domain.Enums;

namespace Jobs.Application.Features.JobPostings.Dto
{
    /// <summary>
    /// Request DTO for updating a job posting.
    /// </summary>
    public class UpdateJobPostingRequest
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public JobCategory? Category { get; set; }

        public int? MinHoursPerWeek { get; set; }

        public int? MaxHoursPerWeek { get; set; }

        public bool? AllowsRemoteWork { get; set; }

        public string? ContactEmail { get; set; }

        public string? JobUrl { get; set; }
    }
}

using Asp.Versioning;
using Jobs.Application.Common;
using Jobs.Application.Features.JobPostings;
using Jobs.Application.Features.JobPostings.Dto;
using Jobs.Application.Pagination;
using Jobs.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.API.Controllers
{
    /// <summary>
    /// Controller for managing job postings (CRUD operations).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route($"{Constants.ApiPrefix}/v{{version:apiVersion}}/[controller]")]
    public class JobPostingController : ControllerBase
    {
        private readonly IJobPostingService _jobPostingService;

        public JobPostingController(IJobPostingService jobPostingService)
        {
            _jobPostingService = jobPostingService;
        }

        /// <summary>
        /// Gets a job posting by its unique identifier.
        /// </summary>
        /// <param name="id">The job posting ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The job posting details.</returns>
        [HttpGet("{id}", Name = "GetJobPostingById")]
        [ProducesResponseType<JobPostingResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetJobPostingById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _jobPostingService.GetJobPostingByIdAsync(id, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Gets a paginated list of all job postings.
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters.</param>
        /// <param name="filteringOptions">Filtering options.</param>
        /// <param name="companyId">Optional company ID filter.</param>
        /// <param name="category">Optional job category filter.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Paginated list of job postings.</returns>
        [HttpGet(Name = "GetAllJobPostings")]
        [ProducesResponseType<PaginatedListResult<JobPostingResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllJobPostings(
            [FromQuery] PaginationParameters paginationParameters,
            [FromQuery] FilteringOptions filteringOptions,
            [FromQuery] Guid? companyId = null,
            [FromQuery] JobCategory? category = null,
            CancellationToken cancellationToken = default)
        {
            var result = await _jobPostingService.GetJobPostingsPaginatedAsync(
                paginationParameters, filteringOptions, companyId, category, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Creates a new job posting.
        /// </summary>
        /// <param name="createJobPosting">The create request.</param>
        /// <returns>The created job posting.</returns>
        [HttpPost(Name = "CreateJobPosting")]
        [ProducesResponseType<JobPostingResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateJobPosting([FromBody] CreateJobPostingRequest createJobPosting)
        {
            var result = await _jobPostingService.CreateJobPostingAsync(createJobPosting, CancellationToken.None);

            return CreatedAtAction(nameof(GetJobPostingById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing job posting.
        /// </summary>
        /// <param name="id">The job posting ID.</param>
        /// <param name="updateJobPosting">The update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated job posting.</returns>
        [HttpPut("{id}", Name = "UpdateJobPosting")]
        [ProducesResponseType<JobPostingResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateJobPosting(Guid id, [FromBody] UpdateJobPostingRequest updateJobPosting, CancellationToken cancellationToken)
        {
            var result = await _jobPostingService.UpdateJobPostingAsync(id, updateJobPosting, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Deletes a job posting by its unique identifier.
        /// </summary>
        /// <param name="id">The job posting ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>No content if the job posting was deleted.</returns>
        [HttpDelete("{id}", Name = "DeleteJobPosting")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteJobPosting(Guid id, CancellationToken cancellationToken)
        {
            await _jobPostingService.DeleteJobPostingAsync(id, cancellationToken);

            return NoContent();
        }
    }
}

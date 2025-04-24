using Asp.Versioning;
using Jobs.Application.Common;
using Jobs.Application.Features.Companies;
using Jobs.Application.Features.Companies.Dto;
using Jobs.Application.Features.Companies.Filters;
using Jobs.Application.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.API.Controllers
{
    /// <summary>
    /// Controller for managing companies (CRUD operations).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route($"{Constants.ApiPrefix}/v{{version:apiVersion}}/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Gets a company by its unique identifier.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The company details.</returns>
        [HttpGet("{id}", Name = "GetCompanyById")]
        [ProducesResponseType<CompanyResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _companyService.GetCompanyByIdAsync(id, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Gets a paginated list of all companies.
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters.</param>
        /// <param name="filteringOptions">Filtering options.</param>
        /// <param name="companyFilter"> The company filter.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Paginated list of companies.</returns>
        [HttpGet(Name = "GetAllCompanies")]
        [ProducesResponseType<PaginatedListResult<CompanyResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCompanies([FromQuery] PaginationParameters paginationParameters, [FromQuery] FilteringOptions filteringOptions, [FromQuery] CompanyFilter companyFilter, CancellationToken cancellationToken)
        {
            var result = await _companyService.GetCompaniesPaginatedAsync(paginationParameters, filteringOptions, companyFilter, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Updates an existing company.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <param name="updateCompany">The update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated company.</returns>
        [HttpPut("{id}", Name = "UpdateCompany")]
        [ProducesResponseType<CompanyResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyRequest updateCompany, CancellationToken cancellationToken)
        {
            var result = await _companyService.UpdateCompanyAsync(id, updateCompany, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Creates a new company.
        /// </summary>
        /// <param name="createCompany">The create request.</param>
        /// <returns>The created company.</returns>
        [HttpPost(Name = "CreateCompany")]
        [ProducesResponseType<CompanyResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequest createCompany)
        {
            var result = await _companyService.CreateCompanyAsync(createCompany, CancellationToken.None);

            return CreatedAtAction(nameof(GetCompanyById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Deletes a company by its unique identifier.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>No content if the company was deleted.</returns>
        [HttpDelete("{id}", Name = "DeleteCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCompany(Guid id, CancellationToken cancellationToken)
        {
            await _companyService.DeleteCompanyAsync(id, cancellationToken);

            return NoContent();
        }
    }
}

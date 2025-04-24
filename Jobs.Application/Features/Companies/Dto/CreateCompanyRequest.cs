using Jobs.Domain.ValueObjects;

namespace Jobs.Application.Features.Companies.Dto
{
    /// <summary>
    /// Request DTO for creating a company.
    /// </summary>
    public record CreateCompanyRequest
    {
        public string Name { get; set; } = null!;

        public Location Location { get; set; } = null!;

        public string? Website { get; set; }
    }
}

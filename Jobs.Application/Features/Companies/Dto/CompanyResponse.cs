using Jobs.Domain.ValueObjects;

namespace Jobs.Application.Features.Companies.Dto
{
    /// <summary>
    /// Response DTO for a company.
    /// </summary>
    public class CompanyResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public Location Location { get; set; } = null!;

        public string? Website { get; set; }
    }
}

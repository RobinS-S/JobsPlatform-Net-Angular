namespace Jobs.Application.Features.Companies.Filters
{
    public record CompanyFilter
    {
        public int MinActiveJobs { get; set; } = 0;
    }
}

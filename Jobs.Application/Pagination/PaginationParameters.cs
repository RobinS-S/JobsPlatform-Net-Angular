namespace Jobs.Application.Pagination
{
    /// <summary>
    /// Parameters for paginating API results.
    /// </summary>
    /// <remarks>
    /// PageNumber is the 1-based index of the page to retrieve. PageSize is the number of items per page (default 25, max 100).
    /// </remarks>
    public record PaginationParameters
    {
        /// <summary>
        /// Gets or sets the 1-based index of the page to retrieve. Must be greater than 0.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of items per page. Must be between 1 and 100.
        /// </summary>
        public int PageSize { get; set; } = 25;
    }
}

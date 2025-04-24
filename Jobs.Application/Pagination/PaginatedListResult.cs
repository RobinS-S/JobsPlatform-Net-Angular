namespace Jobs.Application.Pagination
{
    /// <summary>
    /// Represents a paginated result set.
    /// </summary>
    /// <typeparam name="T">The type of items in the result set.</typeparam>
    public readonly record struct PaginatedListResult<T>
    {
        public IReadOnlyList<T> Items { get; init; }

        public int TotalCount { get; init; }

        public int PageNumber { get; init; }

        public int PageSize { get; init; }

        public int TotalPages { get; init; }
    }
}

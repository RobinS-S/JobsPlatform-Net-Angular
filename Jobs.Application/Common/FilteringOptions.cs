namespace Jobs.Application.Common
{
    /// <summary>
    /// Options for filtering API results.
    /// </summary>
    public record FilteringOptions
    {
        /// <summary>
        /// Gets or sets the optional search text for filtering results.
        /// </summary>
        public string? SearchText { get; set; }
    }
}

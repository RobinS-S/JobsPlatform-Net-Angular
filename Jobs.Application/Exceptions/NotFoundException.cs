namespace Jobs.Application.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string? message = null)
            : base(message ?? "The given resource was not found.")
        {
        }
    }
}

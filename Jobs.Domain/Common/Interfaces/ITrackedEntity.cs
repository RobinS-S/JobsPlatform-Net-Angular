namespace Jobs.Domain.Common.Interfaces
{
    public interface ITrackedEntity : IEntityBase
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

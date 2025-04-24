using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Jobs.Domain.Common.Interfaces;

namespace Jobs.Infrastructure.Data.Repositories
{
    public class EfCoreRepository<T> : RepositoryBase<T>, IRepositoryBase<T>
        where T : class, IEntityBase
    {
        public EfCoreRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}

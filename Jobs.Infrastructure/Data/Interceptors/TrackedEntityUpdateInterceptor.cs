using Jobs.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Jobs.Infrastructure.Data.Interceptors
{
    public sealed class TrackedEntityUpdateInterceptor : SaveChangesInterceptor
    {
        private readonly TimeProvider _clock;

        public TrackedEntityUpdateInterceptor(TimeProvider clock)
        {
            _clock = clock ?? TimeProvider.System;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken ct = default)
        {
            foreach (EntityEntry entry in eventData.Context!.ChangeTracker.Entries<ITrackedEntity>())
            {
                if (entry.State is EntityState.Modified)
                {
                    ((ITrackedEntity)entry.Entity).UpdatedAt = _clock.GetUtcNow().UtcDateTime;
                }
            }

            return base.SavingChangesAsync(eventData, result, ct);
        }
    }
}

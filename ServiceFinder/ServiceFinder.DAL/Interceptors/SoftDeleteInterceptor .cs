using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ServiceFinder.DAL.Interceptors.Interfaces;

namespace ServiceFinder.DAL.Interceptors
{
    internal sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                ApplySoftDelete(eventData.Context);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static void ApplySoftDelete(DbContext context)
        {
            var entities = context.ChangeTracker.Entries<ISoftDeleteEntity>().ToList();

            foreach (var entry in entities)
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues[nameof(ISoftDeleteEntity.IsDeleted)] = true;
                    entry.CurrentValues[nameof(IAuditableEntity.UpdatedAt)] = DateTime.UtcNow;
                }
            }
        }
    }
}


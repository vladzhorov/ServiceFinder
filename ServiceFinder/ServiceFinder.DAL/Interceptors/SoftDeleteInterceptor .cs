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
            var utcNow = DateTime.UtcNow;
            var entities = context.ChangeTracker.Entries<ISoftDeleteEntity>().ToList();

            foreach (var entry in entities)
            {
                if (entry.State == EntityState.Deleted)
                {
                    if (entry.Entity is ISoftDeleteEntity softDeleteEntity)
                    {
                        softDeleteEntity.IsDeleted = true;
                        entry.State = EntityState.Modified;

                        if (entry.Entity is IAuditableEntity auditableEntity)
                        {
                            auditableEntity.UpdatedAt = utcNow;
                        }
                    }
                }
            }
        }
    }
}
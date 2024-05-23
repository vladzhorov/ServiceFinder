using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ServiceFinder.DAL.Interceptors.Interfaces;

namespace ServiceFinder.DAL.Interceptors
{
    internal sealed class UpdateAuditableInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                UpdateAuditableEntities(eventData.Context);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static void UpdateAuditableEntities(DbContext context)
        {
            DateTime utcNow = DateTime.UtcNow;
            var entities = context.ChangeTracker.Entries<IAuditableEntity>().ToList();

            foreach (var entry in entities)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.CurrentValues[nameof(IAuditableEntity.CreatedAt)] = utcNow;
                    entry.CurrentValues[nameof(IAuditableEntity.UpdatedAt)] = utcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.CurrentValues[nameof(IAuditableEntity.UpdatedAt)] = utcNow;
                }
            }

        }
    }
}

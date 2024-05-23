using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ServiceFinder.DAL.Entites;

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
            var entities = context.ChangeTracker.Entries()
                .Where(e => e.Entity is UserProfileEntity || e.Entity is ServiceEntity || e.Entity is ServiceCategoryEntity || e.Entity is ReviewEntity)
                .ToList();

            foreach (var entry in entities)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = utcNow;
                    entry.Property("UpdatedAt").CurrentValue = utcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedAt").CurrentValue = utcNow;
                }
            }
        }
    }
}
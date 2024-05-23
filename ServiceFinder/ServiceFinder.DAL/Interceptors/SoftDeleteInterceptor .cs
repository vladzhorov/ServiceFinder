using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ServiceFinder.DAL.Entites;

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
            var entities = context.ChangeTracker.Entries()
                .Where(e => e.Entity is UserProfileEntity || e.Entity is ServiceEntity || e.Entity is ServiceCategoryEntity || e.Entity is ReviewEntity)
                .ToList();

            foreach (var entry in entities)
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Property("IsDeleted").CurrentValue = true;
                    entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ServiceFinder.DAL.Entites;

namespace ServiceFinder.DAL.Interceptors
{
    internal sealed class UpdateRatingInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                UpdateUserProfileRating(eventData.Context);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static void UpdateUserProfileRating(DbContext context)
        {
            var reviewEntries = context.ChangeTracker.Entries<ReviewEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList();

            foreach (var reviewEntry in reviewEntries)
            {
                var userProfileId = reviewEntry.Entity.UserProfileId;

                var userProfile = context.Set<UserProfileEntity>()
                    .AsNoTracking()
                    .Include(u => u.Reviews)
                    .FirstOrDefault(u => u.Id == userProfileId);

                if (userProfile != null)
                {
                    userProfile.Rating = userProfile.Reviews.Any() ? userProfile.Reviews.Average(r => r.Rating) : 0;
                    context.Set<UserProfileEntity>().Attach(userProfile);
                    context.Entry(userProfile).Property(u => u.Rating).IsModified = true;
                }
            }
        }
    }
}

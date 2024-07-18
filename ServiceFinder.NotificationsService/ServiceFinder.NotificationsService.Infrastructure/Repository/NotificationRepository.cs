using ServiceFinder.NotificationsService.Domain.Interfaces;
using ServiceFinder.NotificationsService.Domain.Models;

namespace ServiceFinder.NotificationsService.Infrastructure.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationDbContext _dbContext;

        public NotificationRepository(NotificationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Notification> AddAsync(Notification notification, CancellationToken cancellationToken)
        {
            await _dbContext.Notifications.AddAsync(notification, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return notification;
        }

        public async Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Notifications.FindAsync(new object[] { id }, cancellationToken);
        }
    }
}
using ServiceFinder.NotificationsService.Domain.Models;

namespace ServiceFinder.NotificationsService.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> AddAsync(Notification notification, CancellationToken cancellationToken);
        Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}

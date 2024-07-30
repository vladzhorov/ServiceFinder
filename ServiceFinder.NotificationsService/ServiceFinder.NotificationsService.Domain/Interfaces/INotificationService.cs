using ServiceFinder.NotificationsService.Domain.Events;

namespace ServiceFinder.NotificationsService.Domain.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(OrderCreatedEvent orderCreatedEvent);
        Task SendNotificationAsync(OrderUpdatedEvent orderUpdatedEvent);
    }
}

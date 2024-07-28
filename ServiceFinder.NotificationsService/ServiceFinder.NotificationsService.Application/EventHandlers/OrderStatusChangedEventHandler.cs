using MediatR;
using ServiceFinder.NotificationsService.Domain.Interfaces;
using ServiceFinder.Shared.Events;

namespace ServiceFinder.NotificationsService.Application.EventHandlers
{
    public class OrderStatusChangedEventHandler : INotificationHandler<OrderStatusChangedEvent>
    {
        private readonly INotificationService _notificationService;

        public OrderStatusChangedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public Task Handle(OrderStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            return _notificationService.SendNotificationAsync(notification);
        }
    }
}

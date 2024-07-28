using MediatR;
using ServiceFinder.NotificationsService.Domain.Interfaces;
using ServiceFinder.Shared.Events;

namespace ServiceFinder.NotificationsService.Application.EventHandlers
{
    public class OrderRequestStatusChangedEventHandler : INotificationHandler<OrderRequestStatusChangedEvent>
    {
        private readonly INotificationService _notificationService;

        public OrderRequestStatusChangedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public Task Handle(OrderRequestStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            return _notificationService.SendNotificationAsync(notification);
        }
    }
}

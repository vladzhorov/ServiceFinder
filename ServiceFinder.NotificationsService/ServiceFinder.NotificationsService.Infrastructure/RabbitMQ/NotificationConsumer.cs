using MassTransit;
using ServiceFinder.NotificationsService.Domain.Events;
using ServiceFinder.NotificationsService.Domain.Interfaces;

namespace ServiceFinder.NotificationService.Infrastructure.RabbitMQ
{
    public class NotificationConsumer :
        IConsumer<OrderCreatedEvent>,
        IConsumer<OrderUpdatedEvent>
    {
        private readonly INotificationService _notificationService;

        public NotificationConsumer(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            return _notificationService.SendNotificationAsync(context.Message);
        }

        public Task Consume(ConsumeContext<OrderUpdatedEvent> context)
        {
            return _notificationService.SendNotificationAsync(context.Message);
        }
    }
}

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

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            await _notificationService.SendNotificationAsync(context.Message);
        }

        public async Task Consume(ConsumeContext<OrderUpdatedEvent> context)
        {
            await _notificationService.SendNotificationAsync(context.Message);
        }
    }
}

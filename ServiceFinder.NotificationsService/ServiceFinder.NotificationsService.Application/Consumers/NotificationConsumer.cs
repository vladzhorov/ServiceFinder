using MassTransit;
using ServiceFinder.NotificationsService.Domain.Events;
using ServiceFinder.NotificationsService.Domain.Interfaces;

namespace ServiceFinder.NotificationService.Application.Consumers
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
            try
            {
                await _notificationService.SendNotificationAsync(context.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process OrderCreatedEvent: {ex.Message}");
                throw;
            }
        }

        public async Task Consume(ConsumeContext<OrderUpdatedEvent> context)
        {
            try
            {
                await _notificationService.SendNotificationAsync(context.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process OrderUpdatedEvent: {ex.Message}");
                throw;
            }
        }
    }
}

using ServiceFinder.NotificationService.Application.Interfaces;
using ServiceFinder.NotificationService.Domain.Interfaces;
using ServiceFinder.NotificationsService.Domain.Events;
using ServiceFinder.NotificationsService.Domain.Interfaces;

namespace ServiceFinder.NotificationService.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly IEventBus _eventBus;

        public NotificationService(IEmailSender emailSender, IEventBus eventBus)
        {
            _emailSender = emailSender;
            _eventBus = eventBus;
        }

        public async Task SendNotificationAsync(OrderCreatedEvent orderCreatedEvent)
        {
            var subject = "Order Created";
            var body = $"Your order with ID {orderCreatedEvent.OrderId} was created at {orderCreatedEvent.CreatedAt}.";
            await _emailSender.SendEmailAsync(orderCreatedEvent.Email, subject, body);

            // Publish event for further processing
            await _eventBus.PublishAsync(orderCreatedEvent);
        }

        public async Task SendNotificationAsync(OrderUpdatedEvent orderUpdatedEvent)
        {
            var subject = "Order Updated";
            var body = $"Your order with ID {orderUpdatedEvent.OrderId} was updated at {orderUpdatedEvent.UpdatedAt}.";
            await _emailSender.SendEmailAsync(orderUpdatedEvent.Email, subject, body);

            // Publish event for further processing
            await _eventBus.PublishAsync(orderUpdatedEvent);
        }
    }
}

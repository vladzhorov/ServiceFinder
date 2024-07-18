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
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task SendNotificationAsync(OrderCreatedEvent orderCreatedEvent)
        {
            if (orderCreatedEvent.Email == null)
            {
                throw new ArgumentNullException(nameof(orderCreatedEvent.Email), "Email cannot be null.");
            }

            var subject = "Order Created";
            var body = $"Your order with ID {orderCreatedEvent.OrderId} was created at {orderCreatedEvent.CreatedAt}.";
            await _emailSender.SendEmailAsync(orderCreatedEvent.Email, subject, body);

            await _eventBus.PublishAsync(orderCreatedEvent);
        }

        public async Task SendNotificationAsync(OrderUpdatedEvent orderUpdatedEvent)
        {
            if (orderUpdatedEvent.Email == null)
            {
                throw new ArgumentNullException(nameof(orderUpdatedEvent.Email), "Email cannot be null.");
            }

            var subject = "Order Updated";
            var body = $"Your order with ID {orderUpdatedEvent.OrderId} was updated at {orderUpdatedEvent.UpdatedAt}.";
            await _emailSender.SendEmailAsync(orderUpdatedEvent.Email, subject, body);

            await _eventBus.PublishAsync(orderUpdatedEvent);
        }
    }
}

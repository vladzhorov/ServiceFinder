using ServiceFinder.OrderService.Application.Messaging;
using ServiceFinder.OrderService.Domain.Events;
using System.Text.Json;

namespace ServiceFinder.OrderService.Application.EventHandlers
{
    public class OrderStatusChangedEventHandler
    {
        private readonly MessagePublisher _publisher;

        public OrderStatusChangedEventHandler(MessagePublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(OrderStatusChangedEvent domainEvent)
        {
            var message = JsonSerializer.Serialize(domainEvent);
            _publisher.Publish(message);
        }
    }
}

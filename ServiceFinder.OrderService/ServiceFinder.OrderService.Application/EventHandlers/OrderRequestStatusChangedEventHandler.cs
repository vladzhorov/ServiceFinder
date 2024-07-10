using ServiceFinder.OrderService.Application.Messaging;
using ServiceFinder.OrderService.Domain.Events;
using System.Text.Json;

namespace ServiceFinder.OrderService.Application.EventHandlers
{
    public class OrderRequestStatusChangedEventHandler
    {
        private readonly MessagePublisher _publisher;

        public OrderRequestStatusChangedEventHandler(MessagePublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(OrderRequestStatusChangedEvent domainEvent)
        {
            var message = JsonSerializer.Serialize(domainEvent);
            _publisher.Publish(message);
        }
    }
}

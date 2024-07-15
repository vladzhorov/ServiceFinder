using ServiceFinder.OrderService.Application.Interfaces;
using ServiceFinder.OrderService.Domain.Events;
using ServiceFinder.OrderService.Domain.Messaging;
using System.Text.Json;

namespace ServiceFinder.OrderService.Application.EventHandlers
{
    public class OrderRequestStatusChangedEventHandler : IOrderRequestStatusChangedEventHandler
    {
        private readonly IMessagePublisher _publisher;

        public OrderRequestStatusChangedEventHandler(IMessagePublisher publisher)
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

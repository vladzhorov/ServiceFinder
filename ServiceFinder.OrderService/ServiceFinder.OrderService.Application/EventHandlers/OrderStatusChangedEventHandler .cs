using MediatR;
using ServiceFinder.OrderService.Domain.Messaging;
using ServiceFinder.Shared.Events;

namespace ServiceFinder.OrderService.Application.EventHandlers
{
    public class OrderStatusChangedEventHandler : INotificationHandler<OrderStatusChangedEvent>
    {
        private readonly IMessagePublisher _publisher;

        public OrderStatusChangedEventHandler(IMessagePublisher publisher)
        {
            _publisher = publisher;
        }
        public async Task Handle(OrderStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            await _publisher.PublishAsync(notification);
        }
    }
}

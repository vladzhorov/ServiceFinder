using MediatR;
using ServiceFinder.OrderService.Domain.Messaging;
using ServiceFinder.Shared.Events;

namespace ServiceFinder.OrderService.Application.EventHandlers
{
    public class OrderRequestStatusChangedEventHandler : INotificationHandler<OrderRequestStatusChangedEvent>
    {
        private readonly IMessagePublisher _publisher;

        public OrderRequestStatusChangedEventHandler(IMessagePublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task Handle(OrderRequestStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            await _publisher.PublishAsync(notification);
        }
    }
}

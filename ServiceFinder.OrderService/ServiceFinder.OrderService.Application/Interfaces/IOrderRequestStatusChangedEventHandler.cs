using ServiceFinder.OrderService.Domain.Events;

namespace ServiceFinder.OrderService.Application.Interfaces
{
    public interface IOrderRequestStatusChangedEventHandler
    {
        void Handle(OrderRequestStatusChangedEvent domainEvent);
    }
}

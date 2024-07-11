using ServiceFinder.OrderService.Domain.Events;

namespace ServiceFinder.OrderService.Application.Interfaces
{
    public interface IOrderStatusChangedEventHandler
    {
        void Handle(OrderStatusChangedEvent domainEvent);
    }
}

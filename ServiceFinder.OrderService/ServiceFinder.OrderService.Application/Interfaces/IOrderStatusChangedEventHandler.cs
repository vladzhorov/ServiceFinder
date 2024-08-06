using ServiceFinder.Shared.Events;

namespace ServiceFinder.OrderService.Application.Interfaces
{
    public interface IOrderStatusChangedEventHandler
    {
        Task HandleAsync(OrderStatusChangedEvent domainEvent);
    }
}

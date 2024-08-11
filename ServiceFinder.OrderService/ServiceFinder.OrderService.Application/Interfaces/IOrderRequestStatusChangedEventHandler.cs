using ServiceFinder.Shared.Events;

namespace ServiceFinder.OrderService.Application.Interfaces
{
    public interface IOrderRequestStatusChangedEventHandler
    {
        Task HandleAsync(OrderRequestStatusChangedEvent domainEvent);
    }
}

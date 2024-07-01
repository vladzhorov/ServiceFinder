using ServiceFinder.OrderService.Domain.Models;

namespace ServiceFinder.OrderService.Domain.Events
{
    public class OrderRequestStatusChangedEvent : IDomainEvent
    {
        public OrderRequest OrderRequest { get; }

        public OrderRequestStatusChangedEvent(OrderRequest orderRequest)
        {
            OrderRequest = orderRequest;
        }
    }
}
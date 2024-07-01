using ServiceFinder.OrderService.Domain.Models;


namespace ServiceFinder.OrderService.Domain.Events
{
    public class OrderStatusChangedEvent : IDomainEvent
    {
        public Order Order { get; }

        public OrderStatusChangedEvent(Order order)
        {
            Order = order;
        }
    }
}
using ServiceFinder.OrderService.Domain.Enums;


namespace ServiceFinder.OrderService.Domain.Events
{
    public class OrderStatusChangedEvent : IDomainEvent
    {
        public Guid OrderId { get; }
        public OrderStatus NewStatus { get; }

        public OrderStatusChangedEvent(Guid orderId, OrderStatus newStatus)
        {
            OrderId = orderId;
            NewStatus = newStatus;
        }
    }
}
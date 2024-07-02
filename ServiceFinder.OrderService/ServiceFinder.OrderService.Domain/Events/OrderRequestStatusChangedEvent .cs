using ServiceFinder.OrderService.Domain.Enums;

namespace ServiceFinder.OrderService.Domain.Events
{
    public class OrderRequestStatusChangedEvent : IDomainEvent
    {
        public Guid OrderRequestId { get; }
        public OrderRequestStatus NewStatus { get; }

        public OrderRequestStatusChangedEvent(Guid orderRequestId, OrderRequestStatus newStatus)
        {
            OrderRequestId = orderRequestId;
            NewStatus = newStatus;
        }
    }
}
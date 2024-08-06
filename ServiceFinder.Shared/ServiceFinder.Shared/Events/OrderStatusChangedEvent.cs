using MediatR;
using ServiceFinder.Shared.Enums;

namespace ServiceFinder.Shared.Events
{
    public class OrderStatusChangedEvent : INotification
    {
        public Guid OrderId { get; }
        public OrderStatus NewStatus { get; }
        public string Email { get; }

        public OrderStatusChangedEvent(Guid orderId, OrderStatus newStatus, string email)
        {
            OrderId = orderId;
            NewStatus = newStatus;
            Email = email;
        }
    }
}
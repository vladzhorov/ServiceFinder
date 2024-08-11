using MediatR;
using ServiceFinder.Shared.Enums;

namespace ServiceFinder.Shared.Events
{
    public class OrderRequestStatusChangedEvent : INotification
    {
        public Guid OrderRequestId { get; }
        public OrderRequestStatus NewStatus { get; }
        public string Email { get; }

        public OrderRequestStatusChangedEvent(Guid orderRequestId, OrderRequestStatus newStatus, string email)
        {
            OrderRequestId = orderRequestId;
            NewStatus = newStatus;
            Email = email;
        }
    }
}
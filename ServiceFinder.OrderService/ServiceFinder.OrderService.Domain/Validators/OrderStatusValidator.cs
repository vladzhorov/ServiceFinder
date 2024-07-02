using ServiceFinder.OrderService.Domain.Enums;

namespace ServiceFinder.OrderService.Domain.Validators
{
    public static class OrderStatusValidator
    {
        private static readonly HashSet<(OrderStatus, OrderStatus)> ValidOrderStatusTransitions = new HashSet<(OrderStatus, OrderStatus)>
        {
            (OrderStatus.Pending, OrderStatus.Confirmed),
            (OrderStatus.Pending, OrderStatus.Cancelled),
            (OrderStatus.Confirmed, OrderStatus.InProgress),
            (OrderStatus.Confirmed, OrderStatus.Cancelled),
            (OrderStatus.InProgress, OrderStatus.Completed),
            (OrderStatus.InProgress, OrderStatus.Cancelled)
        };

        private static readonly HashSet<(OrderRequestStatus, OrderRequestStatus)> ValidOrderRequestStatusTransitions = new HashSet<(OrderRequestStatus, OrderRequestStatus)>
        {
            (OrderRequestStatus.Pending, OrderRequestStatus.Approved),
            (OrderRequestStatus.Pending, OrderRequestStatus.Rejected),
            (OrderRequestStatus.Approved, OrderRequestStatus.Cancelled)
        };

        public static void ValidateStatusTransition(OrderStatus currentStatus, OrderStatus newStatus)
        {
            if (currentStatus == newStatus) return;

            if (!ValidOrderStatusTransitions.Contains((currentStatus, newStatus)))
            {
                throw new InvalidOperationException($"Invalid convert {currentStatus} to {newStatus}");
            }
        }

        public static void ValidateStatusTransition(OrderRequestStatus currentStatus, OrderRequestStatus newStatus)
        {
            if (currentStatus == newStatus) return;

            if (!ValidOrderRequestStatusTransitions.Contains((currentStatus, newStatus)))
            {
                throw new InvalidOperationException($"Invalid convert {currentStatus} to {newStatus}");
            }
        }
    }
}

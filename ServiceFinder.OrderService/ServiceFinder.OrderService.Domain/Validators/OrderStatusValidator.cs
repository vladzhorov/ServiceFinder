using ServiceFinder.OrderService.Domain.Enums;

namespace ServiceFinder.OrderService.Domain.Validators
{
    public static class OrderStatusValidator
    {
        private static readonly Dictionary<OrderStatus, OrderStatus[]> ValidOrderStatusTransitions = new Dictionary<OrderStatus, OrderStatus[]>
        {
            { OrderStatus.Pending, new OrderStatus[] { OrderStatus.Confirmed, OrderStatus.Cancelled } },
            { OrderStatus.Confirmed, new OrderStatus[] { OrderStatus.InProgress, OrderStatus.Cancelled } },
            { OrderStatus.InProgress, new OrderStatus[] { OrderStatus.Completed, OrderStatus.Cancelled } },
        };

        private static readonly Dictionary<OrderRequestStatus, OrderRequestStatus[]> ValidOrderRequestStatusTransitions = new Dictionary<OrderRequestStatus, OrderRequestStatus[]>
        {
            { OrderRequestStatus.Pending, new OrderRequestStatus[] { OrderRequestStatus.Approved, OrderRequestStatus.Rejected } },
            { OrderRequestStatus.Approved, new OrderRequestStatus[] { OrderRequestStatus.Cancelled } },
        };

        public static void ValidateStatusTransition(OrderStatus currentStatus, OrderStatus newStatus)
        {
            if (currentStatus == newStatus)
                return;

            if (!ValidOrderStatusTransitions.TryGetValue(currentStatus, out var validTransitions) || Array.IndexOf(validTransitions, newStatus) == -1)
            {
                throw new InvalidOperationException($"Invalid convert {currentStatus} to {newStatus}");
            }
        }

        public static void ValidateStatusTransition(OrderRequestStatus currentStatus, OrderRequestStatus newStatus)
        {
            if (currentStatus == newStatus)
                return;

            if (!ValidOrderRequestStatusTransitions.TryGetValue(currentStatus, out var validTransitions) || Array.IndexOf(validTransitions, newStatus) == -1)
            {
                throw new InvalidOperationException($"Invalid convert {currentStatus} to {newStatus}");
            }
        }
    }
}

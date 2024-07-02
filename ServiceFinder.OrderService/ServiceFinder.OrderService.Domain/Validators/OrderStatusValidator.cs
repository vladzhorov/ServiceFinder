using ServiceFinder.OrderService.Domain.Enums;
using System.Collections.Immutable;

namespace ServiceFinder.OrderService.Domain.Validators
{
    public static class OrderStatusValidator
    {
        private static readonly ImmutableDictionary<OrderStatus, ImmutableArray<OrderStatus>> ValidOrderStatusTransitions =
            new Dictionary<OrderStatus, OrderStatus[]>
            {
                { OrderStatus.Pending, new OrderStatus[] { OrderStatus.Confirmed, OrderStatus.Cancelled } },
                { OrderStatus.Confirmed, new OrderStatus[] { OrderStatus.InProgress, OrderStatus.Cancelled } },
                { OrderStatus.InProgress, new OrderStatus[] { OrderStatus.Completed, OrderStatus.Cancelled } }
            }.ToImmutableDictionary(kv => kv.Key, kv => kv.Value.ToImmutableArray());

        private static readonly ImmutableDictionary<OrderRequestStatus, ImmutableArray<OrderRequestStatus>> ValidOrderRequestStatusTransitions =
            new Dictionary<OrderRequestStatus, OrderRequestStatus[]>
            {
                { OrderRequestStatus.Pending, new OrderRequestStatus[] { OrderRequestStatus.Approved, OrderRequestStatus.Rejected } },
                { OrderRequestStatus.Approved, new OrderRequestStatus[] { OrderRequestStatus.Cancelled } }
            }.ToImmutableDictionary(kv => kv.Key, kv => kv.Value.ToImmutableArray());

        public static void ValidateStatusTransition(OrderStatus currentStatus, OrderStatus newStatus)
        {
            if (currentStatus == newStatus)
                return;

            if (!ValidOrderStatusTransitions.TryGetValue(currentStatus, out var validTransitions) || !validTransitions.Contains(newStatus))
            {
                throw new InvalidOperationException($"Invalid convert {currentStatus} to {newStatus}");
            }
        }

        public static void ValidateStatusTransition(OrderRequestStatus currentStatus, OrderRequestStatus newStatus)
        {
            if (currentStatus == newStatus)
                return;

            if (!ValidOrderRequestStatusTransitions.TryGetValue(currentStatus, out var validTransitions) || !validTransitions.Contains(newStatus))
            {
                throw new InvalidOperationException($"Invalid convert {currentStatus} to {newStatus}");
            }
        }
    }
}

using ServiceFinder.OrderService.Domain.Enums;
using System.Collections.Immutable;

namespace ServiceFinder.OrderService.Domain.Validators
{
    public static class OrderStatusValidator
    {
        private static readonly ImmutableDictionary<OrderStatus, ImmutableArray<OrderStatus>> ValidOrderStatusTransitions =
                 ImmutableDictionary<OrderStatus, ImmutableArray<OrderStatus>>.Empty
                .Add(OrderStatus.Pending, [OrderStatus.Confirmed, OrderStatus.Cancelled])
                .Add(OrderStatus.Confirmed, [OrderStatus.InProgress, OrderStatus.Cancelled])
                .Add(OrderStatus.InProgress, [OrderStatus.Completed, OrderStatus.Cancelled]);

        private static readonly ImmutableDictionary<OrderRequestStatus, ImmutableArray<OrderRequestStatus>> ValidOrderRequestStatusTransitions =
            ImmutableDictionary<OrderRequestStatus, ImmutableArray<OrderRequestStatus>>.Empty
                .Add(OrderRequestStatus.Pending, [OrderRequestStatus.Approved, OrderRequestStatus.Rejected])
                .Add(OrderRequestStatus.Approved, [OrderRequestStatus.Cancelled]);

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

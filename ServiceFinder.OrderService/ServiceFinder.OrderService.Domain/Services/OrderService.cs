using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Events;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.OrderService.Domain.Validators;

namespace ServiceFinder.OrderService.Domain.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public OrderService(IOrderRepository orderRepository, IDomainEventDispatcher domainEventDispatcher)
        {
            _orderRepository = orderRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);

            OrderStatusValidator.ValidateStatusTransition(order.Status, newStatus);

            order.Status = newStatus;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order, cancellationToken);
            _domainEventDispatcher.Dispatch(new OrderStatusChangedEvent(order.Id, newStatus));
        }

        public async Task CreateOrderAsync(Order order, decimal baseRate, int baseDurationMinutes, CancellationToken cancellationToken)
        {
            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;

            order.Price = CalculatePrice(baseRate, baseDurationMinutes, order.DurationInMinutes);

            await _orderRepository.AddAsync(order, cancellationToken);
        }

        /// <summary>
        /// Calculates the total cost of the order based on the base rate per minute and the total duration of the service
        /// </summary>
        /// <param name="baseRate">Base bet per minute specified in the ad</param>
        /// <param name="baseDurationMinutes">The total duration, in minutes, for which the base rate is specified</param>
        /// <param name="totalDurationInMinutes">The actual total duration of the service, in minutes</param>
        private decimal CalculatePrice(decimal baseRate, int baseDurationMinutes, int totalDurationInMinutes)
        {
            var ratePerMinute = baseRate / baseDurationMinutes;
            var totalPrice = Math.Round(ratePerMinute * totalDurationInMinutes, 2);

            return totalPrice;
        }
    }
}
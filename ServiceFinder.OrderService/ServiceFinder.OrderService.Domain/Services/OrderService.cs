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

        public async Task CreateOrderAsync(Order order, decimal ratePerMinutes, int rateDurationMinutes, CancellationToken cancellationToken)
        {
            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;

            order.Price = CalculatePrice(ratePerMinutes, rateDurationMinutes, order.DurationInMinutes);

            await _orderRepository.AddAsync(order, cancellationToken);
        }

        private decimal CalculatePrice(decimal ratePerMinutes, int rateDurationMinutes, int durationInMinutes)
        {
            decimal ratePerMinute = ratePerMinutes / rateDurationMinutes;
            return Math.Round(ratePerMinute * durationInMinutes, 2);
        }
    }
}
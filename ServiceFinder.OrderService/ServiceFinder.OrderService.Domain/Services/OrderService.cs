using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Events;
using ServiceFinder.OrderService.Domain.Exceptions;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.OrderService.Domain.Providers;
using ServiceFinder.OrderService.Domain.Validators;

namespace ServiceFinder.OrderService.Domain.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IDateTimeProvider _dateTimeProvider;

        public OrderService(IOrderRepository orderRepository, IDomainEventDispatcher domainEventDispatcher, IDateTimeProvider dateTimeProvider)
        {
            _orderRepository = orderRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken)
        {
            var utcNow = _dateTimeProvider.UtcNow;
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);

            if (order == null)
            {
                throw new ModelNotFoundException(orderId);
            }
            OrderStatusValidator.ValidateStatusTransition(order.Status, newStatus);

            order.Status = newStatus;
            order.UpdatedAt = utcNow;

            await _orderRepository.UpdateAsync(order, cancellationToken);
            _domainEventDispatcher.Dispatch(new OrderStatusChangedEvent(order.Id, newStatus));
        }

        public async Task CreateOrderAsync(Order order, decimal baseRatePerMinute, int baseRateDurationInMinutes, CancellationToken cancellationToken)
        {
            var utcNow = _dateTimeProvider.UtcNow;
            order.CreatedAt = utcNow;
            order.UpdatedAt = utcNow;
            order.Status = OrderStatus.Pending;

            order.Price = CalculatePrice(baseRatePerMinute, baseRateDurationInMinutes, order.DurationInMinutes);

            await _orderRepository.AddAsync(order, cancellationToken);
        }

        private decimal CalculatePrice(decimal baseRatePerMinute, int baseRateDurationInMinutes, int totalServiceDurationInMinutes)
        {
            var ratePerMinute = baseRatePerMinute / baseRateDurationInMinutes;
            var totalPrice = Math.Round(ratePerMinute * totalServiceDurationInMinutes, 2);

            return totalPrice;
        }
    }
}
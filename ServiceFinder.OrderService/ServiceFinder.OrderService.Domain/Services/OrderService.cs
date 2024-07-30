using MediatR;
using ServiceFinder.OrderService.Domain.Exceptions;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.OrderService.Domain.Providers;
using ServiceFinder.OrderService.Domain.Validators;
using ServiceFinder.Shared.Enums;
using ServiceFinder.Shared.Events;

namespace ServiceFinder.OrderService.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProfileService _userProfileService;

        public OrderService(IOrderRepository orderRepository,
                            IMediator mediator,
                            IDateTimeProvider dateTimeProvider,
                            IUserProfileService userProfileService)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
            _dateTimeProvider = dateTimeProvider;
            _userProfileService = userProfileService;
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, string email, CancellationToken cancellationToken)
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

            var domainEvent = new OrderStatusChangedEvent(order.Id, newStatus, email);
            await _mediator.Publish(domainEvent, cancellationToken);
        }

        public async Task CreateOrderAsync(Order order, decimal baseRatePerMinute, int baseRateDurationInMinutes, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileService.GetUserProfileAsync(order.CustomerId);
            var assistance = await _userProfileService.GetAssistanceAsync(order.ServiceId);

            if (userProfile == null || assistance == null)
            {
                throw new ModelNotFoundException("UserProfile or Assistance not found.");
            }

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

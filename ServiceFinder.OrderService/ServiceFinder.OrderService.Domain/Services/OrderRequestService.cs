using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Events;
using ServiceFinder.OrderService.Domain.Exceptions;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.OrderService.Domain.Providers;
using ServiceFinder.OrderService.Domain.Validators;

namespace ServiceFinder.OrderService.Domain.Services
{
    public class OrderRequestService
    {
        private readonly IOrderRequestRepository _orderRequestRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IDateTimeProvider _dateTimeProvider;

        public OrderRequestService(IOrderRequestRepository orderRequestRepository, IDomainEventDispatcher domainEventDispatcher, IDateTimeProvider dateTimeProvider)
        {
            _orderRequestRepository = orderRequestRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task UpdateOrderRequestStatusAsync(Guid orderRequestId, OrderRequestStatus newStatus, CancellationToken cancellationToken)
        {
            var utcNow = _dateTimeProvider.UtcNow;

            var orderRequest = await _orderRequestRepository.GetByIdAsync(orderRequestId, cancellationToken);

            if (orderRequest == null)
            {
                throw new ModelNotFoundException(orderRequestId);
            }
            OrderStatusValidator.ValidateStatusTransition(orderRequest.Status, newStatus);

            orderRequest.Status = newStatus;
            orderRequest.UpdatedAt = utcNow;

            await _orderRequestRepository.UpdateAsync(orderRequest, cancellationToken);
            _domainEventDispatcher.Dispatch(new OrderRequestStatusChangedEvent(orderRequest.Id, newStatus));
        }

        public async Task CreateOrderRequestAsync(OrderRequest orderRequest, CancellationToken cancellationToken)
        {
            var utcNow = _dateTimeProvider.UtcNow;
            orderRequest.CreatedAt = utcNow;
            orderRequest.UpdatedAt = utcNow;
            orderRequest.Status = OrderRequestStatus.Pending;

            await _orderRequestRepository.AddAsync(orderRequest, cancellationToken);
        }
    }
}
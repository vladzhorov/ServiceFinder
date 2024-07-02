using OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Events;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.OrderService.Domain.Validators;

namespace ServiceFinder.OrderService.Domain.Services
{
    public class OrderRequestService
    {
        private readonly IOrderRequestRepository _orderRequestRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public OrderRequestService(IOrderRequestRepository orderRequestRepository, IDomainEventDispatcher domainEventDispatcher)
        {
            _orderRequestRepository = orderRequestRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task UpdateOrderRequestStatusAsync(Guid orderRequestId, OrderRequestStatus newStatus, CancellationToken cancellationToken)
        {
            var orderRequest = await _orderRequestRepository.GetByIdAsync(orderRequestId, cancellationToken);

            OrderStatusValidator.ValidateStatusTransition(orderRequest.Status, newStatus);

            orderRequest.Status = newStatus;
            orderRequest.UpdatedAt = DateTime.UtcNow;

            await _orderRequestRepository.UpdateAsync(orderRequest, cancellationToken);
            _domainEventDispatcher.Dispatch(new OrderRequestStatusChangedEvent(orderRequest.Id, newStatus));
        }

        public async Task CreateOrderRequestAsync(OrderRequest orderRequest, CancellationToken cancellationToken)
        {
            orderRequest.CreatedAt = DateTime.UtcNow;
            orderRequest.UpdatedAt = DateTime.UtcNow;
            orderRequest.Status = OrderRequestStatus.Pending;

            await _orderRequestRepository.AddAsync(orderRequest, cancellationToken);
        }
    }
}
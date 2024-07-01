using OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Events;
using ServiceFinder.OrderService.Domain.Interfaces;

namespace OrderService.Domain.Services
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

        public async Task UpdateOrderRequestStatusAsync(Guid orderRequestId, OrderStatus newStatus, CancellationToken cancellationToken)
        {
            var orderRequest = await _orderRequestRepository.GetByIdAsync(orderRequestId, cancellationToken);

            orderRequest.Status = newStatus;
            orderRequest.UpdatedAt = DateTime.UtcNow;

            await _orderRequestRepository.UpdateAsync(orderRequest, cancellationToken);
            _domainEventDispatcher.Dispatch(new OrderRequestStatusChangedEvent(orderRequest));
        }
    }
}

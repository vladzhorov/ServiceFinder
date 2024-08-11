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
    public class OrderRequestService : IOrderRequestService
    {
        private readonly IOrderRequestRepository _orderRequestRepository;
        private readonly IMediator _mediator;
        private readonly IDateTimeProvider _dateTimeProvider;

        public OrderRequestService(IOrderRequestRepository orderRequestRepository, IMediator mediator, IDateTimeProvider dateTimeProvider)
        {
            _orderRequestRepository = orderRequestRepository;
            _mediator = mediator;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task UpdateOrderRequestStatusAsync(Guid orderRequestId, OrderRequestStatus newStatus, string email, CancellationToken cancellationToken)
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

            var domainEvent = new OrderRequestStatusChangedEvent(orderRequest.Id, newStatus, email);
            await _mediator.Publish(domainEvent, cancellationToken);
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

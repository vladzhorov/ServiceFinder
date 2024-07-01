using OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Events;
using ServiceFinder.OrderService.Domain.Interfaces;

namespace OrderService.Domain.Services
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

            order.Status = newStatus;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order, cancellationToken);
            _domainEventDispatcher.Dispatch(new OrderStatusChangedEvent(order));
        }
    }
}

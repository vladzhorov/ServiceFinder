using MassTransit;
using MediatR;
using ServiceFinder.NotificationsService.Domain.Events;
using ServiceFinder.Shared.Events;

namespace ServiceFinder.NotificationService.Application.Consumers
{
    public class OrderStatusChangedEventConsumer :
        IConsumer<OrderStatusChangedEvent>
    {
        private readonly IMediator _mediator;

        public OrderStatusChangedEventConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
        {
            try
            {
                await _mediator.Send(new SendEmailCommand(context.Message.Email!, "Order Created", $"Your order with ID {context.Message.OrderId} was created"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process OrderStatusChangedEvent: {ex.Message}");
                throw;
            }
        }
    }
}

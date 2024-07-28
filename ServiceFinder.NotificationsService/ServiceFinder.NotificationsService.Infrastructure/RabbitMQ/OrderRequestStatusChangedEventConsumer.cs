using MassTransit;
using MediatR;
using ServiceFinder.NotificationsService.Domain.Events;
using ServiceFinder.Shared.Events;

namespace ServiceFinder.NotificationService.Application.Consumers
{
    public class OrderRequestStatusChangedEventConsumer :
        IConsumer<OrderRequestStatusChangedEvent>
    {
        private readonly IMediator _mediator;

        public OrderRequestStatusChangedEventConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderRequestStatusChangedEvent> context)
        {
            try
            {
                await _mediator.Send(new SendEmailCommand(context.Message.Email!, "Order Updated", $"Your order with ID {context.Message.OrderRequestId} was updated."));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process OrderRequestStatusChangedEvent: {ex.Message}");
                throw;
            }
        }
    }
}

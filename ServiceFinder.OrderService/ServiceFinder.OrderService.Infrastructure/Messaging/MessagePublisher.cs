using MassTransit;
using ServiceFinder.OrderService.Domain.Messaging;

namespace ServiceFinder.OrderService.Infrastructure.Messaging
{
    public class MessagePublisher : IMessagePublisher
    {

        private readonly IPublishEndpoint _publishEndpoint;

        public MessagePublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public Task PublishAsync<T>(T message)
            where T : class
        {
            return _publishEndpoint.Publish(message);
        }
    }
}

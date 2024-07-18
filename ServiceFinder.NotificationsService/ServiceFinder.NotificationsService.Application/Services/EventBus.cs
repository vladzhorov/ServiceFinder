using MassTransit;
using ServiceFinder.NotificationService.Application.Interfaces;

namespace ServiceFinder.NotificationService.Application.Services
{
    public class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task PublishAsync<T>(T message) where T : class
        {
            return _publishEndpoint.Publish(message);
        }
    }
}

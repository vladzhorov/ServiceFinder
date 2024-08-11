namespace ServiceFinder.OrderService.Domain.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message) where T : class;
    }
}

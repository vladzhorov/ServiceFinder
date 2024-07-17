namespace ServiceFinder.OrderService.Domain.Messaging
{
    public interface IMessagePublisher : IDisposable
    {
        void Publish(string message);
    }
}

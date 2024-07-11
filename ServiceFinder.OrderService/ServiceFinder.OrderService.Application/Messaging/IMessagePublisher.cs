namespace ServiceFinder.OrderService.Application.Messaging
{
    public interface IMessagePublisher : IDisposable
    {
        void Publish(string message);
    }
}

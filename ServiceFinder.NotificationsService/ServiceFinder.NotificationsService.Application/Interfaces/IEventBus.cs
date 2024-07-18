namespace ServiceFinder.NotificationService.Application.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T message) where T : class;
    }
}

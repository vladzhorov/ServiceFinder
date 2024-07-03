namespace ServiceFinder.OrderService.Domain.Providers;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}

namespace ServiceFinder.OrderService.Domain.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow { get; } = DateTime.UtcNow;
}


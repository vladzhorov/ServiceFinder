namespace ServiceFinder.OrderService.Domain.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    private readonly DateTime DateTimeUtcNow = DateTime.UtcNow;

    public DateTime GetDate() => DateTimeUtcNow;
}

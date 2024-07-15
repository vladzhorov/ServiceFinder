using System.Text.Json.Serialization;

namespace ServiceFinder.OrderService.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        InProgress,
        Completed,
        Cancelled
    }
}

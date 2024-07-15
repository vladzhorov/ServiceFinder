using System.Text.Json.Serialization;

namespace ServiceFinder.OrderService.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderRequestStatus
    {
        Pending,
        Approved,
        Rejected,
        Cancelled
    }
}
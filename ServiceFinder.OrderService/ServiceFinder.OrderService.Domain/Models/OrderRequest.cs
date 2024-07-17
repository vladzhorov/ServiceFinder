using ServiceFinder.OrderService.Domain.Enums;

namespace ServiceFinder.OrderService.Domain.Models
{
    public class OrderRequest : BaseModel
    {
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
        public int DurationInMinutes { get; set; }
        public string? Description { get; set; }
        public OrderRequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

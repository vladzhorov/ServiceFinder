using ServiceFinder.Shared.Enums;

namespace ServiceFinder.OrderService.Domain.Models
{
    public class Order : BaseModel
    {
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public string? Description { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime ScheduledDate { get; set; }
        public decimal Price { get; set; }
    }
}

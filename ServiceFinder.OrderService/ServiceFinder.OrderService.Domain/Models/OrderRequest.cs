using ServiceFinder.OrderService.Domain.Enums;

namespace ServiceFinder.OrderService.Domain.Models
{
    public class OrderRequest
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
        public int DurationInMinutes { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}

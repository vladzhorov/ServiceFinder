using ServiceFinder.Shared.Enums;

namespace ServiceFinder.OrderService.Application.DTOs
{
    public class OrderRequestDto
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public OrderRequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

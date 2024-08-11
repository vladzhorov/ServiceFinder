using ServiceFinder.Shared.Enums;

namespace ServiceFinder.OrderService.API.ViewModels.Order
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

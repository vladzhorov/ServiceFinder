using ServiceFinder.OrderService.Domain.Enums;

namespace ServiceFinder.API.ViewModels.OrderRequest
{
    public class OrderRequestViewModel
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public OrderRequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

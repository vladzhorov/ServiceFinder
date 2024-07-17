namespace ServiceFinder.OrderService.API.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        public Guid CustomerId { get; set; }
        public Guid ServiceId { get; set; }
        public string? Description { get; set; }
        public decimal BaseRatePerMinute { get; set; }
        public int BaseRateDurationInMinutes { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
}

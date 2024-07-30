namespace ServiceFinder.NotificationsService.Domain.Events
{
    public class OrderUpdatedEvent
    {
        public Guid OrderId { get; set; }
        public string? Email { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

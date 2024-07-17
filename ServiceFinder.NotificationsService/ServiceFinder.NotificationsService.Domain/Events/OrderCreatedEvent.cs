namespace ServiceFinder.NotificationsService.Domain.Events
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}


using ServiceFinder.Shared.Enums;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ServiceId { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public OrderStatus Status { get; set; }
    public int DurationInMinutes { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

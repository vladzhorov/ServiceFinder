namespace ServiceFinder.OrderService.Domain.Models
{
    public class UserProfile : BaseModel
    {
        public string? PhotoURL { get; set; }
        public string? PhoneNumber { get; set; }
        public float Rating { get; set; }
    }
}

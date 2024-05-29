namespace ServiceFinder.BLL.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string? PhotoURL { get; set; }
        public string? PhoneNumber { get; set; }
        public float Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Assistance>? Assistances { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}

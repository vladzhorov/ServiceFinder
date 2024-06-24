namespace ServiceFinder.BLL.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string? PhotoURL { get; set; }
        public string? PhoneNumber { get; set; }
        public float Rating => Assistances != null && Assistances.Any() ? Assistances.Average(a => a.Rating) : 0;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Assistance>? Assistances { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}

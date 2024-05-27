namespace ServiceFinder.BLL.Models
{
    public class Service
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public string? Title { get; set; }
        public ServiceCategory? Category { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public float Rating { get; set; }
        public string? Location { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public UserProfile? UserProfile { get; set; }
    }
}

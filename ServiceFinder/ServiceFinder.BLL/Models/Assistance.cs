
namespace ServiceFinder.BLL.Models
{
    public class Assistance
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
        public Guid AssistanceCategoryId { get; set; }
        public string? Title { get; set; }
        public AssistanceCategory? AssistanceCategory { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public float Rating { get; set; }
        public string? Location { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserProfile? UserProfile { get; set; }
    }
}

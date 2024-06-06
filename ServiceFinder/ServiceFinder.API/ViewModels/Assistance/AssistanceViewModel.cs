using ServiceFinder.API.ViewModels.Review;

namespace ServiceFinder.API.ViewModels.Assistance
{
    public class AssistanceViewModel
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
        public Guid AssistanceCategoryId { get; set; }
        public string? Title { get; set; }
        public string? AssistanceCategoryName { get; set; }

        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public float Rating { get; set; }
        public string? Location { get; set; }
        public ICollection<ReviewViewModel>? Reviews { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UserProfilePhotoUrl { get; set; }
        public string? UserProfilePhoneNumber { get; set; }
    }
}

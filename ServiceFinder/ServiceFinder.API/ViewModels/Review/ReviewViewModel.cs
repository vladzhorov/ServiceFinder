namespace ServiceFinder.API.ViewModels.Review
{
    public class ReviewViewModel
    {
        public Guid Id { get; set; }
        public Guid AssistanceId { get; set; }
        public Guid UserProfileId { get; set; }
        public float Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

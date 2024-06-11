namespace ServiceFinder.BLL.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid AssistanceId { get; set; }
        public Guid UserProfileId { get; set; }
        public float Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Assistance? Assistance { get; set; }
    }
}

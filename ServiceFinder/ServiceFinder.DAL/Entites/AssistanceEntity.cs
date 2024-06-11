using ServiceFinder.DAL.Interceptors.Interfaces;

namespace ServiceFinder.DAL.Entites
{
    public class AssistanceEntity : BaseEntity, ISoftDeleteEntity, IAuditableEntity
    {

        public Guid UserProfileId { get; set; }
        public Guid AssistanceCategoryId { get; set; }
        public string? Title { get; set; }
        public AssistanceCategoryEntity? AssistanceCategory { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public float Rating { get; set; }
        public string? Location { get; set; }
        public ICollection<ReviewEntity>? Reviews { get; set; } = new List<ReviewEntity>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public UserProfileEntity? UserProfile { get; set; }
    }
}

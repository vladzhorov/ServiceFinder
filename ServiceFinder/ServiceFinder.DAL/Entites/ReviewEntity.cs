using ServiceFinder.DAL.Interceptors.Interfaces;

namespace ServiceFinder.DAL.Entites
{
    public class ReviewEntity : BaseEntity, ISoftDeleteEntity, IAuditableEntity
    {
        public Guid AssistanceId { get; set; }
        public Guid UserProfileId { get; set; }
        public float Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public AssistanceEntity? Assistance { get; set; }
        public UserProfileEntity? UserProfile { get; set; }
    }
}
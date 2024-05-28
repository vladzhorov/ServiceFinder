using ServiceFinder.DAL.Interceptors.Interfaces;

namespace ServiceFinder.DAL.Entites
{
    public class UserProfileEntity : ISoftDeleteEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public string? PhotoURL { get; set; }
        public string? PhoneNumber { get; set; }
        public float Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<AssistanceEntity>? Assistances { get; set; }
        public ICollection<ReviewEntity>? Reviews { get; set; }
    }
}

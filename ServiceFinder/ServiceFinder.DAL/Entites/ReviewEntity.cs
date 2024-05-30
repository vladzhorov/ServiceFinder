using ServiceFinder.DAL.Interceptors.Interfaces;

namespace ServiceFinder.DAL.Entites
{
    public class ReviewEntity : BaseEntity, ISoftDeleteEntity, IAuditableEntity
    {
        public Guid AssistanceId { get; set; }
        public float Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public AssistanceEntity? Assistances { get; set; }
    }
}

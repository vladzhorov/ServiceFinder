using ServiceFinder.DAL.Interceptors.Interfaces;

namespace ServiceFinder.DAL.Entites
{
    public class ReviewEntity : ISoftDeleteEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public float Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ServiceEntity? Service { get; set; }
    }
}

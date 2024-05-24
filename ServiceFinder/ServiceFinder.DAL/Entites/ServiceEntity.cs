

using ServiceFinder.DAL.Interceptors.Interfaces;

namespace ServiceFinder.DAL.Entites
{
    public class ServiceEntity : ISoftDeleteEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public string? Title { get; set; }
        public ServiceCategoryEntity? Category { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public float Rating { get; set; }
        public string? Location { get; set; }
        public ICollection<ReviewEntity>? Reviews { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public UserProfileEntity? UserProfile { get; set; }
    }
}

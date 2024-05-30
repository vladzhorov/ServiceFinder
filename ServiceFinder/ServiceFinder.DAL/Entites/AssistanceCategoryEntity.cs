using ServiceFinder.DAL.Interceptors.Interfaces;

namespace ServiceFinder.DAL.Entites
{
    public class AssistanceCategoryEntity : BaseEntity, ISoftDeleteEntity, IAuditableEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<AssistanceEntity>? Assistances { get; set; }
    }
}

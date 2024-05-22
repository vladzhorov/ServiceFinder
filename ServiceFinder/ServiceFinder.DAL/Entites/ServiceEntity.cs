

namespace ServiceFinder.DAL.Entites
{
    public class ServiceEntity
    {
        public Guid Id { get; set; }
        public Guid UserProfileID { get; set; }
        public Guid ServiceCategoryID { get; set; }
        public string Title { get; set; }
        public ServiceCategoryEntity Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public float Rating { get; set; }
        public string Location { get; set; }
        public ICollection<ReviewEntity> Reviews { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public UserProfileEntity UserProfile { get; set; }
    }


}

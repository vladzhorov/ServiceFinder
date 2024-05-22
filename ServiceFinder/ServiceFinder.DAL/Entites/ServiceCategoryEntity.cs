namespace ServiceFinder.DAL.Entites
{
    public class ServiceCategoryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ServiceEntity> Services { get; set; }
    }


}

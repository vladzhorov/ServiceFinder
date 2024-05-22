namespace ServiceFinder.DAL.Entites
{
    public class UserProfileEntity
    {
        public Guid Id { get; set; }
        public string PhotoURL { get; set; }
        public string PhoneNumber { get; set; }
        public float Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ServiceEntity> Services { get; set; }
        public ICollection<ReviewEntity> Reviews { get; set; }
    }


}

namespace ServiceFinder.BLL.Models
{
    public class AssistanceCategory
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Assistance>? Assistances { get; set; }
    }
}

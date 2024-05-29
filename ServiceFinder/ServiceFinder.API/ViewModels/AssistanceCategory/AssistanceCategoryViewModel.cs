using ServiceFinder.API.ViewModels.Assistance;

namespace ServiceFinder.API.ViewModels.AssistanceCategory
{
    public class AssistanceCategoryViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<AssistanceViewModel>? Assistances { get; set; }
    }
}

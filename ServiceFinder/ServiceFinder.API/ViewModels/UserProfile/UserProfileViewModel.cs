using ServiceFinder.API.ViewModels.Assistance;
using ServiceFinder.API.ViewModels.Review;

namespace ServiceFinder.API.ViewModels.UserProfile
{
    public class UserProfileViewModel
    {
        public Guid Id { get; set; }
        public string? PhotoURL { get; set; }
        public string? PhoneNumber { get; set; }
        public float Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<AssistanceViewModel>? Assistances { get; set; }
        public ICollection<ReviewViewModel>? Reviews { get; set; }
    }
}

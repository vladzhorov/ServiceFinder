namespace ServiceFinder.API.ViewModels.UserProfile
{
    public class CreateUserProfileViewModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? PhotoURL { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

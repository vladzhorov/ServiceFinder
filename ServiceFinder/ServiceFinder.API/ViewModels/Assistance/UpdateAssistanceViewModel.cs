namespace ServiceFinder.API.ViewModels.Assistance
{
    public class UpdateAssistanceViewModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public string? Location { get; set; }
    }
}

﻿namespace ServiceFinder.API.ViewModels.Review
{
    public class CreateReviewViewModel
    {
        public Guid UserProfileId { get; set; }
        public Guid AssistanceId { get; set; }
        public float Rating { get; set; }
        public string? Comment { get; set; }
    }
}

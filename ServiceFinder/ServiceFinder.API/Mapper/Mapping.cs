using AutoMapper;
using ServiceFinder.API.ViewModels.Assistance;
using ServiceFinder.API.ViewModels.AssistanceCategory;
using ServiceFinder.API.ViewModels.Review;
using ServiceFinder.API.ViewModels.UserProfile;
using ServiceFinder.BLL.Models;

namespace ServiceFinder.API.Mapper
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Assistance, AssistanceViewModel>().ReverseMap();
            CreateMap<CreateAssistanceViewModel, Assistance>();
            CreateMap<UpdateAssistanceViewModel, Assistance>();
            CreateMap<AssistanceCategory, AssistanceCategoryViewModel>().ReverseMap();
            CreateMap<CreateAssistanceCategoryViewModel, AssistanceCategory>();
            CreateMap<UpdateAssistanceCategoryViewModel, AssistanceCategory>();
            CreateMap<Review, ReviewViewModel>().ReverseMap();
            CreateMap<CreateReviewViewModel, Review>();
            CreateMap<UserProfile, UserProfileViewModel>().ReverseMap();
            CreateMap<CreateUserProfileViewModel, UserProfile>();
            CreateMap<UpdateUserProfileViewModel, UserProfile>();
        }
    }
}

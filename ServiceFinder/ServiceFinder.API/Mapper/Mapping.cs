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
            CreateMap<CreateAssistanceViewModel, Assistance>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UpdateAssistanceViewModel, Assistance>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<AssistanceCategory, AssistanceCategoryViewModel>().ReverseMap();
            CreateMap<CreateAssistanceCategoryViewModel, AssistanceCategory>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UpdateAssistanceCategoryViewModel, AssistanceCategory>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<Review, ReviewViewModel>().ReverseMap();
            CreateMap<CreateReviewViewModel, Review>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UserProfile, UserProfileViewModel>().ReverseMap();
            CreateMap<CreateUserProfileViewModel, UserProfile>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UpdateUserProfileViewModel, UserProfile>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}

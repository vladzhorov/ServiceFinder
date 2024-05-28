using AutoMapper;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;

namespace ServiceFinder.BLL.Mapper
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserProfileEntity, UserProfile>().ReverseMap();
            CreateMap<ReviewEntity, Review>().ReverseMap();
            CreateMap<AssistanceEntity, Assistance>().ReverseMap();
            CreateMap<AssistanceCategoryEntity, AssistanceCategory>().ReverseMap();
        }
    }
}

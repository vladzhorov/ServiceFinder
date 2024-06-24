using AutoMapper;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.BLL.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserProfileEntity, UserProfile>().ReverseMap();
            CreateMap<ReviewEntity, Review>().ReverseMap();
            CreateMap<AssistanceEntity, Assistance>().ReverseMap();
            CreateMap<AssistanceCategoryEntity, AssistanceCategory>().ReverseMap();
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
        }
    }
}

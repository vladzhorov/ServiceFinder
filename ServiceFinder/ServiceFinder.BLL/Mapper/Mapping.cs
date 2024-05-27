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
            CreateMap<ServiceEntity, Service>().ReverseMap();
            CreateMap<ServiceCategoryEntity, ServiceCategory>().ReverseMap();
        }
    }
}

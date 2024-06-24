using AutoMapper;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;


namespace ServiceFinder.BLL.Services
{
    public class UserProfileService : GenericService<UserProfileEntity, UserProfile>, IUserProfileService
    {
        public UserProfileService(IUserProfileRepository userProfileRepository, IMapper mapper) : base(userProfileRepository, mapper)
        {
        }
    }
}

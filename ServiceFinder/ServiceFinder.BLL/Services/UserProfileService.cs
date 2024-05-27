using AutoMapper;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Repositories;

namespace ServiceFinder.BLL.Services
{
    public class UserProfileService : GenericService<UserProfileEntity, UserProfile>, IUserProfileService
    {
        public UserProfileService(UserProfileRepository userProfileRepository, IMapper mapper) : base(userProfileRepository, mapper)
        {
        }
    }
}

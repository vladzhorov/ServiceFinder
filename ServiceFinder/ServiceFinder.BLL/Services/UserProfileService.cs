using AutoMapper;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.BLL.Services
{
    public class UserProfileService : GenericService<UserProfileEntity, UserProfile>, IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository, IMapper mapper) : base(userProfileRepository, mapper)
        {
            _userProfileRepository = userProfileRepository;
        }

        public override async Task<UserProfile> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _userProfileRepository.GetByIdAsync(id, cancellationToken);
            var userProfile = _mapper.Map<UserProfile>(entity);

            if (userProfile.Assistances != null)
            {
                foreach (var assistance in userProfile.Assistances)
                {
                    if (assistance.Reviews != null)
                    {
                        assistance.Rating = RatingCalculatorService.CalculateRating(assistance, x => x.Reviews);
                    }
                }
            }
            return userProfile;
        }

        public override async Task<PagedResult<UserProfile>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var pagedResult = await _userProfileRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);
            var userProfiles = _mapper.Map<PagedResult<UserProfile>>(pagedResult);

            foreach (var userProfile in userProfiles.Data)
            {
                if (userProfile.Assistances != null)
                {
                    foreach (var assistance in userProfile.Assistances)
                    {
                        if (assistance.Reviews != null)
                        {
                            assistance.Rating = RatingCalculatorService.CalculateRating(assistance, x => x.Reviews);
                        }
                    }
                }
            }
            return userProfiles;
        }
    }
}



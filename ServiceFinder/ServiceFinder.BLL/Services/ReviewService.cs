using AutoMapper;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Exceptions;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.BLL.Services
{
    public class ReviewService : GenericService<ReviewEntity, Review>, IReviewService
    {
        private readonly IAssistanceRepository _assistanceRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public ReviewService(IReviewRepository repository,
                             IAssistanceRepository assistanceRepository,
                             IUserProfileRepository userProfileRepository,
                             IMapper mapper)
            : base(repository, mapper)
        {
            _assistanceRepository = assistanceRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<bool> IsAssistanceExistsAsync(Guid assistanceId, CancellationToken cancellationToken)
        {
            var assistance = await _assistanceRepository.GetByIdAsync(assistanceId, cancellationToken);
            return assistance != null;
        }

        public async Task<bool> IsUserProfileExistsAsync(Guid userProfileId, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileRepository.GetByIdAsync(userProfileId, cancellationToken);
            return userProfile != null;
        }

        public override async Task<Review> CreateAsync(Review model, CancellationToken cancellationToken)
        {
            if (!await IsAssistanceExistsAsync(model.AssistanceId, cancellationToken))
            {
                throw new ModelNotFoundException(model.AssistanceId);
            }

            if (!await IsUserProfileExistsAsync(model.UserProfileId, cancellationToken))
            {
                throw new ModelNotFoundException(model.UserProfileId);
            }

            return await base.CreateAsync(model, cancellationToken);
        }


    }
}

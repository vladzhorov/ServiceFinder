using AutoMapper;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Exceptions;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.BLL.Services
{
    public class AssistanceService : GenericService<AssistanceEntity, Assistance>, IAssistanceService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IAssistanceCategoryRepository _assistanceCategoryRepository;

        public AssistanceService(IAssistanceRepository repository,
                                 IUserProfileRepository userProfileRepository,
                                 IAssistanceCategoryRepository assistanceCategoryRepository,
                                 IMapper mapper)
            : base(repository, mapper)
        {
            _userProfileRepository = userProfileRepository;
            _assistanceCategoryRepository = assistanceCategoryRepository;
        }

        public async Task<bool> UserProfileExistsAsync(Guid userProfileId, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileRepository.GetByIdAsync(userProfileId, cancellationToken);
            return userProfile != null;
        }

        public async Task<bool> AssistanceCategoryExistsAsync(Guid assistanceCategoryId, CancellationToken cancellationToken)
        {
            var assistanceCategory = await _assistanceCategoryRepository.GetByIdAsync(assistanceCategoryId, cancellationToken);
            return assistanceCategory != null;
        }

        public override async Task<Assistance> CreateAsync(Assistance model, CancellationToken cancellationToken)
        {
            if (!await UserProfileExistsAsync(model.UserProfileId, cancellationToken))
            {
                throw new ModelNotFoundException(model.UserProfileId);
            }

            if (!await AssistanceCategoryExistsAsync(model.AssistanceCategoryId, cancellationToken))
            {
                throw new ModelNotFoundException(model.AssistanceCategoryId);
            }
            return await base.CreateAsync(model, cancellationToken);
        }

    }
}

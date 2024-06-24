using AutoMapper;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Exceptions;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.BLL.Services
{
    public class AssistanceService : GenericService<AssistanceEntity, Assistance>, IAssistanceService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IAssistanceCategoryRepository _assistanceCategoryRepository;

        public AssistanceService(IAssistanceRepository repository, IUserProfileRepository userProfileRepository, IAssistanceCategoryRepository assistanceCategoryRepository, IMapper mapper) : base(repository, mapper)
        {
            _userProfileRepository = userProfileRepository;
            _assistanceCategoryRepository = assistanceCategoryRepository;
        }
        public override async Task<Assistance> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var assistanceEntity = await _repository.GetByIdAsync(id, cancellationToken);
            var assistance = _mapper.Map<Assistance>(assistanceEntity);

            if (assistance.Reviews != null)
            {
                assistance.Rating = RatingCalculatorService.CalculateRating(assistance, x => x.Reviews);
            }

            return assistance;
        }

        public override async Task<PagedResult<Assistance>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var pagedResult = await _repository.GetAllAsync(pageNumber, pageSize, cancellationToken);
            var assistances = _mapper.Map<PagedResult<Assistance>>(pagedResult);

            foreach (var assistance in assistances.Data)
            {
                if (assistance.Reviews != null)
                {
                    assistance.Rating = RatingCalculatorService.CalculateRating(assistance, x => x.Reviews);
                }
            }
            return assistances;
        }
        public async Task<bool> IsUserProfileExistsAsync(Guid userProfileId, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileRepository.GetByIdAsync(userProfileId, cancellationToken);
            return userProfile != null;
        }

        public async Task<bool> IsAssistanceCategoryExistsAsync(Guid assistanceCategoryId, CancellationToken cancellationToken)
        {
            var assistanceCategory = await _assistanceCategoryRepository.GetByIdAsync(assistanceCategoryId, cancellationToken);
            return assistanceCategory != null;
        }

        public override async Task<Assistance> CreateAsync(Assistance model, CancellationToken cancellationToken)
        {
            if (!await IsUserProfileExistsAsync(model.UserProfileId, cancellationToken))
            {
                throw new ModelNotFoundException(model.UserProfileId);
            }

            if (!await IsAssistanceCategoryExistsAsync(model.AssistanceCategoryId, cancellationToken))
            {
                throw new ModelNotFoundException(model.AssistanceCategoryId);
            }
            return await base.CreateAsync(model, cancellationToken);
        }

    }
}

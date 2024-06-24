using AutoMapper;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.BLL.Services
{
    public class AssistanceCategoryService : GenericService<AssistanceCategoryEntity, AssistanceCategory>, IAssistanceCategoryService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IAssistanceCategoryRepository _assistanceCategoryRepository;

        public AssistanceCategoryService(IAssistanceCategoryRepository repository,
                                  IUserProfileRepository userProfileRepository,
                                  IMapper mapper)
       : base(repository, mapper)
        {
            _userProfileRepository = userProfileRepository;
            _assistanceCategoryRepository = repository;
        }


        public override async Task<AssistanceCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            var assistanceCategory = _mapper.Map<AssistanceCategory>(entity);

            if (assistanceCategory.Assistances != null)
            {
                foreach (var assistance in assistanceCategory.Assistances)
                {
                    if (assistance.Reviews != null)
                    {
                        assistance.Rating = RatingCalculatorService.CalculateRating(assistance, x => x.Reviews);
                    }
                }
            }

            return assistanceCategory;
        }

        public override async Task<PagedResult<AssistanceCategory>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var pagedResult = await _repository.GetAllAsync(pageNumber, pageSize, cancellationToken);
            var assistanceCategories = _mapper.Map<PagedResult<AssistanceCategory>>(pagedResult);

            foreach (var category in assistanceCategories.Data)
            {
                if (category.Assistances != null)
                {
                    foreach (var assistance in category.Assistances)
                    {
                        if (assistance.Reviews != null)
                        {
                            assistance.Rating = RatingCalculatorService.CalculateRating(assistance, x => x.Reviews);
                        }
                    }
                }
            }

            return assistanceCategories;
        }
    }
}
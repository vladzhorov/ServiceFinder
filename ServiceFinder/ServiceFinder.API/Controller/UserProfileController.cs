using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.API.DI;
using ServiceFinder.API.ViewModels.UserProfile;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;

namespace ServiceFinder.API.Controller
{
    [ApiController]
    [Route(ApiRoutes.UsersProfile)]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserProfileViewModel> _createUserProfileViewModelValidator;
        private readonly IValidator<UpdateUserProfileViewModel> _updateUserProfileViewModelValidator;

        public UserProfileController(IValidator<UpdateUserProfileViewModel> updateUserProfileViewModelValidator,
            IValidator<CreateUserProfileViewModel> createUserProfileViewModelValidator,
            IMapper mapper,
            IUserProfileService userProfileService)
        {
            _createUserProfileViewModelValidator = createUserProfileViewModelValidator;
            _updateUserProfileViewModelValidator = updateUserProfileViewModelValidator;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        [HttpGet]
        public async Task<List<UserProfileViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var usersProfile = await _userProfileService.GetAllAsync(cancellationToken);
            return _mapper.Map<List<UserProfileViewModel>>(usersProfile);
        }

        [HttpGet("{id}")]
        public async Task<UserProfileViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<UserProfileViewModel>(userProfile);
        }

        [HttpPost]
        public async Task<UserProfileViewModel> Create(CreateUserProfileViewModel viewModel, CancellationToken cancellationToken)
        {
            await _createUserProfileViewModelValidator.ValidateAndThrowAsync(viewModel, cancellationToken);
            var userProfile = _mapper.Map<UserProfile>(viewModel);
            var result = await _userProfileService.CreateAsync(userProfile, cancellationToken);
            return _mapper.Map<UserProfileViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<UserProfileViewModel> Update(Guid id, UpdateUserProfileViewModel viewModel, CancellationToken cancellationToken)
        {
            await _updateUserProfileViewModelValidator.ValidateAndThrowAsync(viewModel, cancellationToken);
            var modelToUpdate = _mapper.Map<UserProfile>(viewModel);
            var result = await _userProfileService.UpdateAsync(id, modelToUpdate, cancellationToken);
            return _mapper.Map<UserProfileViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _userProfileService.DeleteAsync(id, cancellationToken);
        }
    }
}
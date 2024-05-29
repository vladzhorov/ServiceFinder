using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.API.ViewModels.UserProfile;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;

namespace ServiceFinder.API.Controller
{
    [ApiController]
    [Route("api/usersProfile")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _UserProfileService;
        private readonly IMapper _mapper;

        public UserProfileController(IMapper mapper, IUserProfileService UserProfileService)
        {
            _mapper = mapper;
            _UserProfileService = UserProfileService;
        }

        [HttpGet]
        public async Task<List<UserProfileViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var UsersProfile = await _UserProfileService.GetAllAsync(cancellationToken);
            return _mapper.Map<List<UserProfileViewModel>>(UsersProfile);
        }

        [HttpGet("{id}")]
        public async Task<UserProfileViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var UserProfile = await _UserProfileService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<UserProfileViewModel>(UserProfile);
        }

        [HttpPost]
        public async Task<UserProfileViewModel> Create(CreateUserProfileViewModel viewModel, CancellationToken cancellationToken)
        {
            var userProfile = _mapper.Map<UserProfile>(viewModel);
            var result = await _UserProfileService.CreateAsync(userProfile, cancellationToken);
            return _mapper.Map<UserProfileViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<UserProfileViewModel> Update(Guid id, UpdateUserProfileViewModel viewModel, CancellationToken cancellationToken)
        {
            var modelToUpdate = _mapper.Map<UserProfile>(viewModel);
            modelToUpdate.Id = id;
            var result = await _UserProfileService.UpdateAsync(modelToUpdate, cancellationToken);
            return _mapper.Map<UserProfileViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _UserProfileService.DeleteAsync(id, cancellationToken);
        }
    }
}
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.API.Constants;
using ServiceFinder.API.ViewModels.UserProfile;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.PaginationObjects;

//[Authorize]
[ApiController]
[Route(ApiRoutes.UsersProfile)]
public class UserProfileController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateUserProfileViewModel> _createUserProfileViewModelValidator;
    private readonly IValidator<UpdateUserProfileViewModel> _updateUserProfileViewModelValidator;
    private readonly IAuthServiceClient _authServiceClient;

    public UserProfileController(IValidator<CreateUserProfileViewModel> createUserProfileViewModelValidator,
        IValidator<UpdateUserProfileViewModel> updateUserProfileViewModelValidator,
        IMapper mapper,
        IUserProfileService userProfileService,
        IAuthServiceClient authServiceClient)
    {
        _createUserProfileViewModelValidator = createUserProfileViewModelValidator;
        _updateUserProfileViewModelValidator = updateUserProfileViewModelValidator;
        _mapper = mapper;
        _userProfileService = userProfileService;
        _authServiceClient = authServiceClient;
    }

    [HttpPost]
    public async Task<UserProfileViewModel> Create(CreateUserProfileViewModel viewModel, CancellationToken cancellationToken)
    {
        await _createUserProfileViewModelValidator.ValidateAndThrowAsync(viewModel, cancellationToken);

        var auth0Id = await _authServiceClient.RegisterUserAsync(viewModel.Email, viewModel.Password);

        var userProfile = _mapper.Map<UserProfile>(viewModel);
        userProfile.Auth0Id = auth0Id;
        var result = await _userProfileService.CreateAsync(userProfile, cancellationToken);

        return _mapper.Map<UserProfileViewModel>(result);
    }

    [HttpGet]
    public async Task<PagedResult<UserProfileViewModel>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var pagedResult = await _userProfileService.GetAllAsync(pageNumber, pageSize, cancellationToken);
        return _mapper.Map<PagedResult<UserProfileViewModel>>(pagedResult);
    }

    [HttpGet("{id}")]
    public async Task<UserProfileViewModel> GetById(Guid id, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileService.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<UserProfileViewModel>(userProfile);
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

    [HttpPost("{userId}/assign-role")]
    public async Task<IActionResult> AssignRole(string userId, [FromBody] string roleName)
    {
        try
        {
            await _authServiceClient.AssignRoleToUserAsync(userId, roleName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{userId}/remove-role")]
    public async Task<IActionResult> RemoveRole(string userId, [FromBody] string roleName)
    {
        try
        {
            await _authServiceClient.RemoveRoleFromUserAsync(userId, roleName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

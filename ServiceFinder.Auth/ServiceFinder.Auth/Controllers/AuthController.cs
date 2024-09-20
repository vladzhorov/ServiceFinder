using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.Auth.Interfaces;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var auth0Id = await _authService.RegisterUserAsync(request.Email, request.Password);
                return Ok(new RegisterResponse { Auth0Id = auth0Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new RegisterResponse
                {
                    Error = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequestViewModel request)
        {
            try
            {
                var token = await _authService.LoginUserAsync(request.Email, request.Password);
                return Ok(new LoginResponse { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new LoginResponse
                {
                    Error = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new LoginResponse
                {
                    Error = ex.Message
                });
            }
        }
        [HttpPost("logout")]
        public async Task<ActionResult> Logout([FromBody] LogoutRequest request)
        {
            try
            {
                await _authService.LogoutUserAsync(request.Token);
                return Ok(new { Success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Error = ex.Message });
            }
        }

        public class LogoutRequest
        {
            public string Token { get; set; }
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<UserResponse>> GetUser(string userId)
        {
            var user = await _authService.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound(new UserResponse
                {
                    Error = "User not found"
                });
            }

            return Ok(new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Roles = user.Roles.Select(r => r.Name).ToList()
            });
        }

        [HttpPost("assign-role")]
        public async Task<ActionResult<AssignRoleResponse>> AssignRole([FromBody] RoleAssignmentRequest request)
        {
            try
            {
                await _authService.AssignRoleToUserAsync(request.UserId, request.RoleName);
                return Ok(new AssignRoleResponse
                {
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AssignRoleResponse
                {
                    Success = false,
                    Error = ex.Message
                });
            }
        }

        [HttpPost("remove-role")]
        public async Task<ActionResult<RemoveRoleResponse>> RemoveRole([FromBody] RoleAssignmentRequest request)
        {
            try
            {
                await _authService.RemoveRoleFromUserAsync(request.UserId, request.RoleName);
                return Ok(new RemoveRoleResponse
                {
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new RemoveRoleResponse
                {
                    Success = false,
                    Error = ex.Message
                });
            }
        }

        public class RoleAssignmentRequest
        {
            public string UserId { get; set; }
            public string RoleName { get; set; }
        }
        public class RegisterResponse
        {
            public string Auth0Id { get; set; }
            public string Error { get; set; }
        }

        public class LoginResponse
        {
            public string Token { get; set; }
            public string Error { get; set; }
        }

        public class UserResponse
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public List<string> Roles { get; set; }
            public string Error { get; set; }
        }
        public sealed class LoginRequestViewModel
        {

            public required string Email { get; init; }
            public required string Password { get; init; }

        }
        public class AssignRoleResponse
        {
            public bool Success { get; set; }
            public string Error { get; set; }
        }

        public class RemoveRoleResponse
        {
            public bool Success { get; set; }
            public string Error { get; set; }
        }
    }
}

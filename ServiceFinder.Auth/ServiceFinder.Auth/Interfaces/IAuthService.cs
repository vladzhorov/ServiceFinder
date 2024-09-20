using ServiceFinder.Auth.Models;

namespace ServiceFinder.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(string email, string password);
        Task<string> LoginUserAsync(string email, string password);
        Task<User> GetUserAsync(string userId);
        Task AssignRoleToUserAsync(string userId, string roleName);
        Task RemoveRoleFromUserAsync(string userId, string roleName);
        Task LogoutUserAsync(string token);
    }
}

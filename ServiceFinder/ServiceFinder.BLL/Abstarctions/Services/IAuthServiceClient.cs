using ServiceFinder.BLL.Models;

namespace ServiceFinder.BLL.Abstarctions.Services
{
    public interface IAuthServiceClient
    {
        Task<string> RegisterUserAsync(string email, string password);
        Task<string> LoginUserAsync(string email, string password);
        Task<User> GetUserByAuth0IdAsync(string auth0Id);
        Task RemoveRoleFromUserAsync(string userId, string roleName);
        Task AssignRoleToUserAsync(string userId, string roleName);
    }
}

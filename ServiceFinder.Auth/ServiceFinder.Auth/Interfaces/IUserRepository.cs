using ServiceFinder.Auth.Models;

namespace ServiceFinder.Auth.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task<User> GetByEmailAsync(string email);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
        Task<Role> GetRoleByNameAsync(string roleName);
    }
}
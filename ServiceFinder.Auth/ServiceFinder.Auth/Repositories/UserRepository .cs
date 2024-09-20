using AuthService.Data;
using Microsoft.EntityFrameworkCore;
using ServiceFinder.Auth.Interfaces;
using ServiceFinder.Auth.Models;

namespace AuthService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _context;

        public UserRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email == email);
        }



        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.Roles).ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

    }
}

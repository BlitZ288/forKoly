using Auth.Application.ExternalServices;
using Auth.Domain.Entities;
using Auth.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckPassword(string userName, string password)
        {
            var result = await _context.Users.AnyAsync(u => u.Name == userName && u.PasswordHash == password);

            return result;
        }

        public async Task<User> CreateUser(User user)
        {
            var newUser = await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return newUser.Entity;
        }

        public async Task<User?> GetUserById(long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<User?> GetUserByName(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == userName);

            return user;
        }

        public async Task<bool> IsExistUserByName(string userName)
        {
            var result = await _context.Users.AnyAsync(u => u.Name == userName);

            return result;
        }
    }
}

using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user, string password);
    Task UpdateUserAsync(User user);
    Task UpdatePasswordAsync(int userId, string newPassword);
    Task DeleteUserAsync(int id);
    Task<bool> ValidatePasswordAsync(string username, string password);
}
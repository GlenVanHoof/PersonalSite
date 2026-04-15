using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user, string password);
    Task UpdateUserAsync(User user);
    Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    Task DeleteUserAsync(int id);
    Task<User?> AuthenticateAsync(string username, string password);
}
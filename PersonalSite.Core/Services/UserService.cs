using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Core.Models;

namespace PersonalSite.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _userRepository.GetUserByUsernameAsync(username);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task<User> CreateUserAsync(User user, string password)
    {
        // Validate username is unique
        var existing = await _userRepository.GetUserByUsernameAsync(user.Username);
        if (existing != null)
            throw new InvalidOperationException($"Username '{user.Username}' already exists.");

        // Validate email is unique
        existing = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existing != null)
            throw new InvalidOperationException($"Email '{user.Email}' already exists.");

        return await _userRepository.CreateUserAsync(user, password);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
            return false;

        // Verify current password
        var isValid = await _userRepository.ValidatePasswordAsync(user.Username, currentPassword);
        if (!isValid)
            return false;

        await _userRepository.UpdatePasswordAsync(userId, newPassword);
        return true;
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteUserAsync(id);
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var isValid = await _userRepository.ValidatePasswordAsync(username, password);
        if (!isValid)
            return null;

        var user = await _userRepository.GetUserByUsernameAsync(username);
        
        // Update last login timestamp
        if (user != null)
        {
            user.LastLoginOn = DateTime.UtcNow;
            await _userRepository.UpdateUserAsync(user);
        }

        return user;
    }
}
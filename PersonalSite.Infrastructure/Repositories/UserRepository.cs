using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PortfolioDbContext _context;

    public UserRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var entities = await _context.Users.ToListAsync();
        return entities.Select(UserMapper.ToDomain);
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var entity = await _context.Users.FindAsync(id);
        return entity == null ? null : UserMapper.ToDomain(entity);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return entity == null ? null : UserMapper.ToDomain(entity);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return entity == null ? null : UserMapper.ToDomain(entity);
    }

    public async Task<User> CreateUserAsync(User user, string password)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        var entity = UserMapper.ToEntity(user, passwordHash);
        
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();

        return UserMapper.ToDomain(entity);
    }

    public async Task UpdateUserAsync(User user)
    {
        var entity = await _context.Users.FindAsync(user.Id);
        if (entity == null)
            throw new KeyNotFoundException($"User with ID {user.Id} not found.");

        entity.Username = user.Username;
        entity.Email = user.Email;
        entity.FirstName = user.FirstName;
        entity.LastName = user.LastName;
        entity.IsActive = user.IsActive;
        entity.Role = user.Role;
        entity.LastLoginOn = user.LastLoginOn;

        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePasswordAsync(int userId, string newPassword)
    {
        var entity = await _context.Users.FindAsync(userId);
        if (entity == null)
            throw new KeyNotFoundException($"User with ID {userId} not found.");

        entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity != null)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ValidatePasswordAsync(string username, string password)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (entity == null)
            return false;

        return BCrypt.Net.BCrypt.Verify(password, entity.PasswordHash);
    }
}
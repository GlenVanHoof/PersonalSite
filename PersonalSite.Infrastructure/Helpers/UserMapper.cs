using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class UserMapper
{
    public static User ToDomain(UserEntity entity)
    {
        return new User
        {
            Id = entity.Id,
            Username = entity.Username,
            Email = entity.Email,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            IsActive = entity.IsActive,
            Role = entity.Role,
            LastLoginOn = entity.LastLoginOn,
            CreatedOn = entity.CreatedOn,
            UpdatedOn = entity.UpdatedOn
            // PasswordHash is intentionally NOT mapped
        };
    }

    public static UserEntity ToEntity(User domain, string? passwordHash = null)
    {
        return new UserEntity
        {
            Id = domain.Id,
            Username = domain.Username,
            Email = domain.Email,
            PasswordHash = passwordHash ?? string.Empty, // Will be set separately
            FirstName = domain.FirstName,
            LastName = domain.LastName,
            IsActive = domain.IsActive,
            Role = domain.Role,
            LastLoginOn = domain.LastLoginOn,
            CreatedOn = domain.CreatedOn,
            UpdatedOn = domain.UpdatedOn
        };
    }
}
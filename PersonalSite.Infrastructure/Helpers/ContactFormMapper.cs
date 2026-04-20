using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class ContactFormMapper
{
    public static Contact ToDomain(ContactFormEntity entity)
    {
        // Split Name into FirstName and LastName
        var nameParts = entity.Name.Split(' ', 2, StringSplitOptions.TrimEntries);
        
        return new Contact
        {
            Id = entity.Id,
            FirstName = nameParts.Length > 0 ? nameParts[0] : entity.Name,
            LastName = nameParts.Length > 1 ? nameParts[1] : null,
            Email = entity.Email,
            Message = entity.Message,
            CreatedOn = entity.CreatedOn
        };
    }

    public static ContactFormEntity ToEntity(Contact domain)
    {
        // Combine FirstName and LastName into Name
        var name = string.IsNullOrWhiteSpace(domain.LastName)
            ? domain.FirstName ?? string.Empty
            : $"{domain.FirstName} {domain.LastName}";

        return new ContactFormEntity
        {
            Id = domain.Id,
            Name = name,
            Email = domain.Email ?? string.Empty,
            Message = domain.Message ?? string.Empty,
            CreatedOn = domain.CreatedOn
        };
    }
}
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class ContactMapper
{
    public static Contact ToModel(ContactEntity entity)
    {
        if (entity == null) return null!;

        return new Contact
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Message = entity.Message,
            CreatedAt = entity.CreatedAt
        };
    }

    public static ContactEntity ToEntity(Contact model)
    {
        if (model == null) return null!;

        return new ContactEntity
        {
            Id = model.Id,
            FirstName = model.FirstName!,
            LastName = model.LastName!,
            Email = model.Email!,
            Message = model.Message!,
            CreatedAt = model.CreatedAt
        };
    }

    public static IEnumerable<Contact> ToModelList(IEnumerable<ContactEntity> entities)
    {
        return entities?.Select(ToModel) ?? Enumerable.Empty<Contact>();
    }

    public static IEnumerable<ContactEntity> ToEntityList(IEnumerable<Contact> models)
    {
        return models?.Select(ToEntity) ?? Enumerable.Empty<ContactEntity>();
    }
}
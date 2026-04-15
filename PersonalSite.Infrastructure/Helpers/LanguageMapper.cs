using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class LanguageMapper
{
    public static Language ToDomain(LanguageEntity entity)
    {
        return new Language
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name
        };
    }

    public static LanguageEntity ToEntity(Language domain)
    {
        return new LanguageEntity
        {
            Id = domain.Id,
            Code = domain.Code,
            Name = domain.Name
        };
    }
}
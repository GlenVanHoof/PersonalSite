using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class ExperienceMapper
{
    public static async Task<Experience> ToDomainAsync(ExperienceEntity entity, TranslationHelper translationHelper)
    {
        var translations = await translationHelper.GetAllTranslationsAsync("Experience", entity.Id);

        return new Experience
        {
            Id = entity.Id,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CreatedOn = entity.CreatedOn,
            UpdatedOn = entity.UpdatedOn,
            Company = translations.GetValueOrDefault("Company") ?? new Dictionary<string, string>(),
            Position = translations.GetValueOrDefault("Position") ?? new Dictionary<string, string>(),
            Description = translations.GetValueOrDefault("Description") ?? new Dictionary<string, string>()
        };
    }

    public static ExperienceEntity ToEntity(Experience domain)
    {
        return new ExperienceEntity
        {
            Id = domain.Id,
            StartDate = domain.StartDate,
            EndDate = domain.EndDate,
            CreatedOn = domain.CreatedOn,
            UpdatedOn = domain.UpdatedOn
        };
    }

    public static Dictionary<string, Dictionary<string, string>> ExtractTranslations(Experience domain)
    {
        return new Dictionary<string, Dictionary<string, string>>
        {
            ["Company"] = domain.Company,
            ["Position"] = domain.Position,
            ["Description"] = domain.Description
        };
    }
}
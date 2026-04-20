using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class EducationMapper
{
    public static async Task<Education> ToDomainAsync(EducationEntity entity, TranslationHelper translationHelper)
    {
        var translations = await translationHelper.GetAllTranslationsAsync("Education", entity.Id);

        return new Education
        {
            Id = entity.Id,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CreatedOn = entity.CreatedOn,
            UpdatedOn = entity.UpdatedOn,
            Institution = translations.GetValueOrDefault("Institution") ?? new Dictionary<string, string>(),
            Degree = translations.GetValueOrDefault("Degree") ?? new Dictionary<string, string>(),
            FieldOfStudy = translations.GetValueOrDefault("FieldOfStudy") ?? new Dictionary<string, string>(),
            Description = translations.GetValueOrDefault("Description") ?? new Dictionary<string, string>()
        };
    }

    public static EducationEntity ToEntity(Education domain)
    {
        return new EducationEntity
        {
            Id = domain.Id,
            StartDate = domain.StartDate,
            EndDate = domain.EndDate,
            CreatedOn = domain.CreatedOn,
            UpdatedOn = domain.UpdatedOn
        };
    }

    public static Dictionary<string, Dictionary<string, string>> ExtractTranslations(Education domain)
    {
        return new Dictionary<string, Dictionary<string, string>>
        {
            ["Institution"] = domain.Institution,
            ["Degree"] = domain.Degree,
            ["FieldOfStudy"] = domain.FieldOfStudy,
            ["Description"] = domain.Description
        };
    }
}
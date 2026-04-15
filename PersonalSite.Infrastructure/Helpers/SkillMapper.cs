using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class SkillMapper
{
    public static async Task<Skill> ToDomainAsync(SkillEntity entity, TranslationHelper translationHelper)
    {
        var translations = await translationHelper.GetAllTranslationsAsync("Skill", entity.Id);

        return new Skill
        {
            Id = entity.Id,
            Type = entity.Type,
            ScoreOutOf100 = entity.ScoreOutOf100,
            CreatedOn = entity.CreatedOn,
            UpdatedOn = entity.UpdatedOn,
            Name = translations.GetValueOrDefault("Name") ?? new Dictionary<string, string>(),
            Description = translations.GetValueOrDefault("Description") ?? new Dictionary<string, string>()
        };
    }

    public static SkillEntity ToEntity(Skill domain)
    {
        return new SkillEntity
        {
            Id = domain.Id,
            Type = domain.Type,
            ScoreOutOf100 = domain.ScoreOutOf100,
            CreatedOn = domain.CreatedOn,
            UpdatedOn = domain.UpdatedOn
        };
    }

    public static Dictionary<string, Dictionary<string, string>> ExtractTranslations(Skill domain)
    {
        return new Dictionary<string, Dictionary<string, string>>
        {
            ["Name"] = domain.Name,
            ["Description"] = domain.Description
        };
    }
}
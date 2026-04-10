using PersonalSite.Core.Enums;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class SkillMapper
{
    public static Skill ToModel(SkillEntity entity)
    {
        if (entity == null) return null;

        return new Skill
        {
            Id = entity.Id,
            Name = entity.Name,
            Type = Enum.Parse<SkillType>(entity.Type),
            ScoreOutOf100 = entity.ScoreOutOf100,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static SkillEntity ToEntity(Skill model)
    {
        if (model == null) return null;

        return new SkillEntity
        {
            Id = model.Id,
            Name = model.Name,
            Type = model.Type.ToString(),
            ScoreOutOf100 = model.ScoreOutOf100,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt
        };
    }

    public static IEnumerable<Skill> ToModelList(IEnumerable<SkillEntity> entities)
    {
        return entities?.Select(ToModel) ?? Enumerable.Empty<Skill>();
    }

    public static IEnumerable<SkillEntity> ToEntityList(IEnumerable<Skill> models)
    {
        return models?.Select(ToEntity) ?? Enumerable.Empty<SkillEntity>();
    }
}
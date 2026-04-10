using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class ExperienceMapper
{
    public static Experience ToModel(ExperienceEntity entity)
    {
        if (entity == null) return null;

        return new Experience
        {
            Id = entity.Id,
            Company = entity.Company,
            Position = entity.Position,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static ExperienceEntity ToEntity(Experience model)
    {
        if (model == null) return null;

        return new ExperienceEntity
        {
            Id = model.Id,
            Company = model.Company,
            Position = model.Position,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Description = model.Description,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt
        };
    }

    public static IEnumerable<Experience> ToModelList(IEnumerable<ExperienceEntity> entities)
    {
        return entities?.Select(ToModel) ?? Enumerable.Empty<Experience>();
    }

    public static IEnumerable<ExperienceEntity> ToEntityList(IEnumerable<Experience> models)
    {
        return models?.Select(ToEntity) ?? Enumerable.Empty<ExperienceEntity>();
    }
}
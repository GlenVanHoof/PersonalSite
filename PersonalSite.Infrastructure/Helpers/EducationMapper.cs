using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class EducationMapper
{
    public static Education ToModel(EducationEntity entity)
    {
        if (entity == null) return null;

        return new Education
        {
            Id = entity.Id,
            Institution = entity.Institution,
            Degree = entity.Degree,
            FieldOfStudy = entity.FieldOfStudy,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static EducationEntity ToEntity(Education model)
    {
        if (model == null) return null;

        return new EducationEntity
        {
            Id = model.Id,
            Institution = model.Institution,
            Degree = model.Degree,
            FieldOfStudy = model.FieldOfStudy,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Description = model.Description,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt
        };
    }

    public static IEnumerable<Education> ToModelList(IEnumerable<EducationEntity> entities)
    {
        return entities?.Select(ToModel) ?? Enumerable.Empty<Education>();
    }

    public static IEnumerable<EducationEntity> ToEntityList(IEnumerable<Education> models)
    {
        return models?.Select(ToEntity) ?? Enumerable.Empty<EducationEntity>();
    }
}
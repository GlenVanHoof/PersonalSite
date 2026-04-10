using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class ProjectTranslationMapper
{
    public static ProjectTranslation ToModel(ProjectTranslationEntity entity)
    {
        if (entity == null) return null!;

        return new ProjectTranslation
        {
            Id = entity.Id,
            ProjectId = entity.ProjectId,
            Language = entity.Language,
            Title = entity.Title,
            ShortDescription = entity.ShortDescription,
            LongDescription = entity.LongDescription,
            Technologies = entity.Technologies
        };
    }

    public static ProjectTranslationEntity ToEntity(ProjectTranslation model)
    {
        if (model == null) return null!;

        return new ProjectTranslationEntity
        {
            Id = model.Id,
            ProjectId = model.ProjectId,
            Language = model.Language,
            Title = model.Title,
            ShortDescription = model.ShortDescription,
            LongDescription = model.LongDescription,
            Technologies = model.Technologies!
        };
    }

    public static IEnumerable<ProjectTranslation> ToModelList(IEnumerable<ProjectTranslationEntity> entities)
    {
        return entities?.Select(ToModel) ?? Enumerable.Empty<ProjectTranslation>();
    }

    public static IEnumerable<ProjectTranslationEntity> ToEntityList(IEnumerable<ProjectTranslation> models)
    {
        return models?.Select(ToEntity) ?? Enumerable.Empty<ProjectTranslationEntity>();
    }
}
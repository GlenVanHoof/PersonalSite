using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class ProjectMapper
{
    public static Project ToModel(ProjectEntity entity)
    {
        if (entity == null) return null!;

        return new Project
        {
            Id = entity.Id,
            Slug = entity.Slug!,
            GitUrl = entity.GithubUrl,
            ImagePath = entity.ImagePath,
            OrderIndex = entity.OrderIndex,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static ProjectEntity ToEntity(Project model)
    {
        if (model == null) return null!;

        return new ProjectEntity
        {
            Id = model.Id,
            Slug = model.Slug!,
            GithubUrl = model.GitUrl!,
            ImagePath = model.ImagePath!,
            OrderIndex = model.OrderIndex,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt
        };
    }

    public static IEnumerable<Project> ToModelList(IEnumerable<ProjectEntity> entities)
    {
        return entities?.Select(ToModel) ?? Enumerable.Empty<Project>();
    }

    public static IEnumerable<ProjectEntity> ToEntityList(IEnumerable<Project> models)
    {
        return models?.Select(ToEntity) ?? Enumerable.Empty<ProjectEntity>();
    }
}
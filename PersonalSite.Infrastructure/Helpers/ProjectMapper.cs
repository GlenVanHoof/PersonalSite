using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class ProjectMapper
{
    /// <summary>
    /// Convert Entity to Domain Model (with translations)
    /// </summary>
    public static async Task<Project> ToDomainAsync(ProjectEntity entity, TranslationHelper translationHelper)
    {
        var translations = await translationHelper.GetAllTranslationsAsync("Project", entity.Id);

        var project = new Project
        {
            Id = entity.Id,
            Slug = entity.Slug,
            GithubUrl = entity.GithubUrl,
            ProjectUrl = entity.ProjectUrl,
            ImagePath = entity.ImagePath,
            OrderIndex = entity.OrderIndex,
            CreatedOn = entity.CreatedOn,
            UpdatedOn = entity.UpdatedOn,
            Title = translations.GetValueOrDefault("Title") ?? new Dictionary<string, string>(),
            Description = translations.GetValueOrDefault("Description") ?? new Dictionary<string, string>(),
            ShortDescription = translations.GetValueOrDefault("ShortDescription") ?? new Dictionary<string, string>(),
            Pictures = entity.Pictures.Select(p => new Picture
            {
                Id = p.Id,
                Source = p.Source,
                ProjectId = p.ProjectId,
                CreatedOn = p.CreatedOn,
                UpdatedOn = p.UpdatedOn
            }).ToList()
        };

        // Map Skills
        project.Skills = new List<Skill>();
        foreach (var skillEntity in entity.Skills)
        {
            var skill = await SkillMapper.ToDomainAsync(skillEntity, translationHelper);
            project.Skills.Add(skill);
        }

        return project;
    }

    /// <summary>
    /// Convert Domain Model to Entity (basic properties only, translations handled separately)
    /// </summary>
    public static ProjectEntity ToEntity(Project domain)
    {
        var entity = new ProjectEntity
        {
            Id = domain.Id,
            Slug = domain.Slug,
            GithubUrl = domain.GithubUrl,
            ProjectUrl = domain.ProjectUrl,
            ImagePath = domain.ImagePath,
            OrderIndex = domain.OrderIndex,
            CreatedOn = domain.CreatedOn,
            UpdatedOn = domain.UpdatedOn
        };

        // Skills are intentionally not mapped here; the repository is responsible
        // for attaching existing skills via properly tracked EF entities.
        entity.Skills = new List<SkillEntity>();

        return entity;
    }

    /// <summary>
    /// Extract translations from domain model
    /// </summary>
    public static Dictionary<string, Dictionary<string, string>> ExtractTranslations(Project domain)
    {
        return new Dictionary<string, Dictionary<string, string>>
        {
            ["Title"] = domain.Title,
            ["Description"] = domain.Description,
            ["ShortDescription"] = domain.ShortDescription
        };
    }
}
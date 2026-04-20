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

        return new Project
        {
            Id = entity.Id,
            Slug = entity.Slug,
            GithubUrl = entity.GithubUrl,
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
    }

    /// <summary>
    /// Convert Domain Model to Entity (basic properties only, translations handled separately)
    /// </summary>
    public static ProjectEntity ToEntity(Project domain)
    {
        return new ProjectEntity
        {
            Id = domain.Id,
            Slug = domain.Slug,
            GithubUrl = domain.GithubUrl,
            ImagePath = domain.ImagePath,
            OrderIndex = domain.OrderIndex,
            CreatedOn = domain.CreatedOn,
            UpdatedOn = domain.UpdatedOn
        };
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
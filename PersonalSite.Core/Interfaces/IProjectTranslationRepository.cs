using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces;

public interface IProjectTranslationRepository
{
    Task<IEnumerable<ProjectTranslation>> GetAllTranslationsAsync();
    Task<IEnumerable<ProjectTranslation>> GetTranslationsByProjectIdAsync(int projectId);
    Task<ProjectTranslation?> GetTranslationByIdAsync(int id);
    Task<ProjectTranslation?> GetTranslationByProjectIdAndLanguageAsync(int projectId, string language);
    Task<ProjectTranslation> CreateTranslationAsync(ProjectTranslation translation);
    Task UpdateTranslationAsync(ProjectTranslation translation);
    Task DeleteTranslationAsync(int id);
}

using PersonalSite.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalSite.Core.Interfaces;

public interface IProjectTranslationService
{
    Task<IEnumerable<ProjectTranslation>> GetAllTranslationsAsync();
    Task<IEnumerable<ProjectTranslation>> GetTranslationsByProjectIdAsync(int projectId);
    Task<ProjectTranslation?> GetTranslationByIdAsync(int id);
    Task<ProjectTranslation?> GetTranslationByProjectIdAndLanguageAsync(int projectId, string language);
    Task<ProjectTranslation> CreateTranslationAsync(ProjectTranslation translation);
    Task UpdateTranslationAsync(ProjectTranslation translation);
    Task DeleteTranslationAsync(int id);
}
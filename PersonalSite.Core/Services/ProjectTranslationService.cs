using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalSite.Core.Services;

public class ProjectTranslationService : IProjectTranslationService
{
    private readonly IProjectTranslationRepository _projectTranslationRepository;

    public ProjectTranslationService(IProjectTranslationRepository projectTranslationRepository)
    {
        _projectTranslationRepository = projectTranslationRepository;
    }

    public async Task<IEnumerable<ProjectTranslation>> GetAllTranslationsAsync()
    {
        return await _projectTranslationRepository.GetAllTranslationsAsync();
    }

    public async Task<IEnumerable<ProjectTranslation>> GetTranslationsByProjectIdAsync(int projectId)
    {
        return await _projectTranslationRepository.GetTranslationsByProjectIdAsync(projectId);
    }

    public async Task<ProjectTranslation?> GetTranslationByIdAsync(int id)
    {
        return await _projectTranslationRepository.GetTranslationByIdAsync(id);
    }

    public async Task<ProjectTranslation?> GetTranslationByProjectIdAndLanguageAsync(int projectId, string language)
    {
        return await _projectTranslationRepository.GetTranslationByProjectIdAndLanguageAsync(projectId, language);
    }

    public async Task<ProjectTranslation> CreateTranslationAsync(ProjectTranslation translation)
    {
        return await _projectTranslationRepository.CreateTranslationAsync(translation);
    }

    public async Task UpdateTranslationAsync(ProjectTranslation translation)
    {
        await _projectTranslationRepository.UpdateTranslationAsync(translation);
    }

    public async Task DeleteTranslationAsync(int id)
    {
        await _projectTranslationRepository.DeleteTranslationAsync(id);
    }
}
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;

namespace PersonalSite.Core.Services;

public class ExperienceService : IExperienceService
{
    private readonly IExperienceRepository _experienceRepository;

    public ExperienceService(IExperienceRepository experienceRepository)
    {
        _experienceRepository = experienceRepository;
    }

    public async Task<IEnumerable<Experience>> GetAllExperiencesAsync()
    {
        return await _experienceRepository.GetAllExperiencesAsync();
    }

    public async Task<Experience?> GetExperienceByIdAsync(int id)
    {
        return await _experienceRepository.GetExperienceByIdAsync(id);
    }

    public async Task<Experience> CreateExperienceAsync(Experience experience)
    {
        return await _experienceRepository.CreateExperienceAsync(experience);
    }

    public async Task UpdateExperienceAsync(Experience experience)
    {
        await _experienceRepository.UpdateExperienceAsync(experience);
    }

    public async Task DeleteExperienceAsync(int id)
    {
        await _experienceRepository.DeleteExperienceAsync(id);
    }
}
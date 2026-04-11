using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces;

public interface IExperienceService
{
    Task<IEnumerable<Experience>> GetAllExperiencesAsync();
    Task<Experience?> GetExperienceByIdAsync(int id);
    Task<Experience> CreateExperienceAsync(Experience experience);
    Task UpdateExperienceAsync(Experience experience);
    Task DeleteExperienceAsync(int id);
}
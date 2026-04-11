using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces;

public interface IExperienceRepository
{
    Task<IEnumerable<Experience>> GetAllExperiencesAsync();
    Task<Experience?> GetExperienceByIdAsync(int id);
    Task<Experience> CreateExperienceAsync(Experience experience);
    Task UpdateExperienceAsync(Experience experience);
    Task DeleteExperienceAsync(int id);
}
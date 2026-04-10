using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Interfaces;

public interface IExperienceRepository
{
    Task<IEnumerable<ExperienceEntity>> GetAllExperiencesAsync();
    Task<ExperienceEntity?> GetExperienceByIdAsync(int id);
    Task<ExperienceEntity> CreateExperienceAsync(ExperienceEntity experience);
    Task UpdateExperienceAsync(ExperienceEntity experience);
    Task DeleteExperienceAsync(int id);
}
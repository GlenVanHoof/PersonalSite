using PersonalSite.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalSite.Core.Interfaces;

public interface IExperienceRepository
{
    Task<IEnumerable<Experience>> GetAllExperiencesAsync();
    Task<Experience?> GetExperienceByIdAsync(int id);
    Task<Experience> CreateExperienceAsync(Experience experience);
    Task UpdateExperienceAsync(Experience experience);
    Task DeleteExperienceAsync(int id);
}
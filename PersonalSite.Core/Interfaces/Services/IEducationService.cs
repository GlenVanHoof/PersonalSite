using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces.Services;

public interface IEducationService
{
    Task<IEnumerable<Education>> GetAllEducationsAsync();
    Task<Education?> GetEducationByIdAsync(int id);
    Task<Education> CreateEducationAsync(Education education);
    Task UpdateEducationAsync(Education education);
    Task DeleteEducationAsync(int id);
    Task<IEnumerable<Education>> GetEducationsOrderedByDateAsync();
}
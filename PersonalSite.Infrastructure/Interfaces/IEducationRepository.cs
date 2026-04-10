using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Interfaces;

public interface IEducationRepository
{
    Task<IEnumerable<EducationEntity>> GetAllEducationsAsync();
    Task<EducationEntity?> GetEducationByIdAsync(int id);
    Task<EducationEntity> CreateEducationAsync(EducationEntity education);
    Task UpdateEducationAsync(EducationEntity education);
    Task DeleteEducationAsync(int id);
}
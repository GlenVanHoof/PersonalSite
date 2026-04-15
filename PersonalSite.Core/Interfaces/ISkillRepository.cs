using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces;

public interface ISkillRepository
{
    Task<IEnumerable<Skill>> GetAllSkillsAsync();
    Task<Skill?> GetSkillByIdAsync(int id);
    Task<IEnumerable<Skill>> GetSkillsByTypeAsync(string type);
    Task<Skill> CreateSkillAsync(Skill skill);
    Task UpdateSkillAsync(Skill skill);
    Task DeleteSkillAsync(int id);
}

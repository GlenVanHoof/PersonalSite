using PersonalSite.Core.Enums;
using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces.Services;

public interface ISkillService
{
    Task<IEnumerable<Skill>> GetAllSkillsAsync();
    Task<Skill?> GetSkillByIdAsync(int id);
    Task<IEnumerable<Skill>> GetSkillsByTypeAsync(SkillType type);
    Task<Skill> CreateSkillAsync(Skill skill);
    Task UpdateSkillAsync(Skill skill);
    Task DeleteSkillAsync(int id);
    Task<IEnumerable<Skill>> GetSkillsOrderedByScoreAsync();
}
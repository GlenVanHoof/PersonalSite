using PersonalSite.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalSite.Core.Interfaces;

public interface ISkillService
{
    Task<IEnumerable<Skill>> GetAllSkillsAsync();
    Task<Skill?> GetSkillByIdAsync(int id);
    Task<Skill> CreateSkillAsync(Skill skill);
    Task UpdateSkillAsync(Skill skill);
    Task DeleteSkillAsync(int id);
}
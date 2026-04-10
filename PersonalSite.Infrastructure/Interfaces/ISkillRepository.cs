using PersonalSite.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalSite.Infrastructure.Interfaces
{
    public interface ISkillRepository
    {
        Task<IEnumerable<SkillEntity>> GetAllSkillsAsync();
        Task<SkillEntity?> GetSkillByIdAsync(int id);
        Task<SkillEntity> CreateSkillAsync(SkillEntity skill);
        Task UpdateSkillAsync(SkillEntity skill);
        Task DeleteSkillAsync(int id);

    }
}

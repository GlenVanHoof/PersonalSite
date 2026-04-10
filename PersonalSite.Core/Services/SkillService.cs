using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalSite.Core.Services;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;

    public SkillService(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
    {
        return await _skillRepository.GetAllSkillsAsync();
    }

    public async Task<Skill?> GetSkillByIdAsync(int id)
    {
        return await _skillRepository.GetSkillByIdAsync(id);
    }

    public async Task<Skill> CreateSkillAsync(Skill skill)
    {
        return await _skillRepository.CreateSkillAsync(skill);
    }

    public async Task UpdateSkillAsync(Skill skill)
    {
        await _skillRepository.UpdateSkillAsync(skill);
    }

    public async Task DeleteSkillAsync(int id)
    {
        await _skillRepository.DeleteSkillAsync(id);
    }
}
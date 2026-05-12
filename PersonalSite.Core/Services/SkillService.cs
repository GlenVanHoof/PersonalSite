using PersonalSite.Core.Enums;
using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Core.Models;

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

    public async Task<IEnumerable<Skill>> GetSkillsByTypeAsync(SkillType type)
    {
        return await _skillRepository.GetSkillsByTypeAsync(type);
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

    public async Task<IEnumerable<Skill>> GetSkillsOrderedByScoreAsync()
    {
        var skills = await _skillRepository.GetAllSkillsAsync();
        return skills.OrderByDescending(s => s.ScoreOutOf100);
    }

    public async Task<Dictionary<SkillType, List<Skill>>> GetSkillsOrderedByTypeAsync()
    {
        var grouped = (await GetSkillsOrderedByScoreAsync())
            .GroupBy(s => s.Type)
            .OrderBy(g => (int)g.Key) // Sort by SkillType-enum order
            .ToDictionary(
                g => g.Key,
                g => g.OrderByDescending(s => s.ScoreOutOf100).ToList()
            );
        return grouped;
    }
}
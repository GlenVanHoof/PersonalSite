using Microsoft.EntityFrameworkCore;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Interfaces;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly PortfolioDbContext _context;

    public SkillRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SkillEntity>> GetAllSkillsAsync()
    {
        return await _context.Skills
            .OrderBy(s => s.Type)
            .ThenByDescending(s => s.ScoreOutOf100)
            .ToListAsync();
    }

    public async Task<SkillEntity?> GetSkillByIdAsync(int id)
    {
        return await _context.Skills.FindAsync(id);
    }

    public async Task<SkillEntity> CreateSkillAsync(SkillEntity skill)
    {
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();
        return skill;
    }

    public async Task UpdateSkillAsync(SkillEntity skill)
    {
        _context.Skills.Update(skill);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSkillAsync(int id)
    {
        var skill = await _context.Skills.FindAsync(id);
        if (skill != null)
        {
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
        }
    }
}
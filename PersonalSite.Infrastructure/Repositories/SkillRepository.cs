using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly PortfolioDbContext _context;

    public SkillRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
    {
        var entities = await _context.Skills
            .OrderBy(s =>
                s.Type == "Technical" ? 1 :
                s.Type == "Tool" ? 2 :
                s.Type == "Soft" ? 3 :
                s.Type == "Language" ? 4 : 5)
            .ThenByDescending(s => s.ScoreOutOf100)
            .ToListAsync();

        return SkillMapper.ToModelList(entities);
    }

    public async Task<Skill?> GetSkillByIdAsync(int id)
    {
        var entity = await _context.Skills.FindAsync(id);
        return entity != null ? SkillMapper.ToModel(entity) : null;
    }

    public async Task<Skill> CreateSkillAsync(Skill skill)
    {
        var entity = SkillMapper.ToEntity(skill);
        _context.Skills.Add(entity);
        await _context.SaveChangesAsync();
        return SkillMapper.ToModel(entity);
    }

    public async Task UpdateSkillAsync(Skill skill)
    {
        var entity = SkillMapper.ToEntity(skill);
        _context.Skills.Update(entity);
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
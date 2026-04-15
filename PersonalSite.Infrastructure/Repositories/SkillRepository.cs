using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly PortfolioDbContext _context;
    private readonly TranslationHelper _translationHelper;

    public SkillRepository(PortfolioDbContext context)
    {
        _context = context;
        _translationHelper = new TranslationHelper(context);
    }

    public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
    {
        var entities = await _context.Skills.ToListAsync();
        var skills = new List<Skill>();
        
        foreach (var entity in entities)
        {
            skills.Add(await SkillMapper.ToDomainAsync(entity, _translationHelper));
        }
        
        return skills;
    }

    public async Task<Skill?> GetSkillByIdAsync(int id)
    {
        var entity = await _context.Skills.FindAsync(id);
        return entity == null ? null : await SkillMapper.ToDomainAsync(entity, _translationHelper);
    }

    public async Task<IEnumerable<Skill>> GetSkillsByTypeAsync(string type)
    {
        var entities = await _context.Skills.Where(s => s.Type == type).ToListAsync();
        var skills = new List<Skill>();
        
        foreach (var entity in entities)
        {
            skills.Add(await SkillMapper.ToDomainAsync(entity, _translationHelper));
        }
        
        return skills;
    }

    public async Task<Skill> CreateSkillAsync(Skill skill)
    {
        var entity = SkillMapper.ToEntity(skill);
        _context.Skills.Add(entity);
        await _context.SaveChangesAsync();

        var translations = SkillMapper.ExtractTranslations(skill);
        await _translationHelper.SaveAllTranslationsAsync("Skill", entity.Id, translations);

        return await GetSkillByIdAsync(entity.Id) ?? skill;
    }

    public async Task UpdateSkillAsync(Skill skill)
    {
        var entity = await _context.Skills.FindAsync(skill.Id);
        if (entity == null)
            throw new KeyNotFoundException($"Skill with ID {skill.Id} not found.");

        entity.Type = skill.Type;
        entity.ScoreOutOf100 = skill.ScoreOutOf100;

        _context.Skills.Update(entity);
        await _context.SaveChangesAsync();

        var translations = SkillMapper.ExtractTranslations(skill);
        await _translationHelper.SaveAllTranslationsAsync("Skill", entity.Id, translations);
    }

    public async Task DeleteSkillAsync(int id)
    {
        var entity = await _context.Skills.FindAsync(id);
        if (entity != null)
        {
            await _translationHelper.DeleteTranslationsAsync("Skill", id);
            _context.Skills.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
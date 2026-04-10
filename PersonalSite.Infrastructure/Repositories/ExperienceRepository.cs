using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Models;
using PersonalSite.Core.Interfaces;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class ExperienceRepository : IExperienceRepository
{
    private readonly PortfolioDbContext _context;

    public ExperienceRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Experience>> GetAllExperiencesAsync()
    {
        var entities = await _context.Experiences
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();
        
        return ExperienceMapper.ToModelList(entities);
    }

    public async Task<Experience?> GetExperienceByIdAsync(int id)
    {
        var entity = await _context.Experiences.FindAsync(id);
        return entity != null ? ExperienceMapper.ToModel(entity) : null;
    }

    public async Task<Experience> CreateExperienceAsync(Experience experience)
    {
        var entity = ExperienceMapper.ToEntity(experience);
        _context.Experiences.Add(entity);
        await _context.SaveChangesAsync();
        return ExperienceMapper.ToModel(entity);
    }

    public async Task UpdateExperienceAsync(Experience experience)
    {
        var entity = ExperienceMapper.ToEntity(experience);
        _context.Experiences.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteExperienceAsync(int id)
    {
        var experience = await _context.Experiences.FindAsync(id);
        if (experience != null)
        {
            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Interfaces;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Repositories;

public class ExperienceRepository : IExperienceRepository
{
    private readonly PortfolioDbContext _context;

    public ExperienceRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExperienceEntity>> GetAllExperiencesAsync()
    {
        return await _context.Experiences
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();
    }

    public async Task<ExperienceEntity?> GetExperienceByIdAsync(int id)
    {
        return await _context.Experiences.FindAsync(id);
    }

    public async Task<ExperienceEntity> CreateExperienceAsync(ExperienceEntity experience)
    {
        _context.Experiences.Add(experience);
        await _context.SaveChangesAsync();
        return experience;
    }

    public async Task UpdateExperienceAsync(ExperienceEntity experience)
    {
        _context.Experiences.Update(experience);
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
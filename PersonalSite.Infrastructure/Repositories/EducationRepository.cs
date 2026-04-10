using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Models;
using PersonalSite.Core.Interfaces;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class EducationRepository : IEducationRepository
{
    private readonly PortfolioDbContext _context;

    public EducationRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Education>> GetAllEducationsAsync()
    {
        var entities = await _context.Educations
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();
        
        return EducationMapper.ToModelList(entities);
    }

    public async Task<Education?> GetEducationByIdAsync(int id)
    {
        var entity = await _context.Educations.FindAsync(id);
        return entity != null ? EducationMapper.ToModel(entity) : null;
    }

    public async Task<Education> CreateEducationAsync(Education education)
    {
        var entity = EducationMapper.ToEntity(education);
        _context.Educations.Add(entity);
        await _context.SaveChangesAsync();
        return EducationMapper.ToModel(entity);
    }

    public async Task UpdateEducationAsync(Education education)
    {
        var entity = EducationMapper.ToEntity(education);
        _context.Educations.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEducationAsync(int id)
    {
        var education = await _context.Educations.FindAsync(id);
        if (education != null)
        {
            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();
        }
    }
}
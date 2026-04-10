using Microsoft.EntityFrameworkCore;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Interfaces;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Repositories;

public class EducationRepository : IEducationRepository
{
    private readonly PortfolioDbContext _context;

    public EducationRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EducationEntity>> GetAllEducationsAsync()
    {
        return await _context.Educations
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();
    }

    public async Task<EducationEntity?> GetEducationByIdAsync(int id)
    {
        return await _context.Educations.FindAsync(id);
    }

    public async Task<EducationEntity> CreateEducationAsync(EducationEntity education)
    {
        _context.Educations.Add(education);
        await _context.SaveChangesAsync();
        return education;
    }

    public async Task UpdateEducationAsync(EducationEntity education)
    {
        _context.Educations.Update(education);
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
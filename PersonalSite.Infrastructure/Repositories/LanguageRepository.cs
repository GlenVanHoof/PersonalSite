using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class LanguageRepository : ILanguageRepository
{
    private readonly PortfolioDbContext _context;

    public LanguageRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
    {
        var entities = await _context.Languages.OrderBy(l => l.Code).ToListAsync();
        return entities.Select(LanguageMapper.ToDomain);
    }

    public async Task<Language?> GetLanguageByIdAsync(int id)
    {
        var entity = await _context.Languages.FindAsync(id);
        return entity == null ? null : LanguageMapper.ToDomain(entity);
    }

    public async Task<Language?> GetLanguageByCodeAsync(string code)
    {
        var entity = await _context.Languages.FirstOrDefaultAsync(l => l.Code == code);
        return entity == null ? null : LanguageMapper.ToDomain(entity);
    }
}
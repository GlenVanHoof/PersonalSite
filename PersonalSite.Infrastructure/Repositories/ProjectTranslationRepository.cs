using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class ProjectTranslationRepository : IProjectTranslationRepository
{
    private readonly PortfolioDbContext _context;

    public ProjectTranslationRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectTranslation>> GetAllTranslationsAsync()
    {
        var entities = await _context.ProjectTranslations
            .OrderBy(t => t.ProjectId)
            .ThenBy(t => t.Language)
            .ToListAsync();

        return ProjectTranslationMapper.ToModelList(entities);
    }

    public async Task<IEnumerable<ProjectTranslation>> GetTranslationsByProjectIdAsync(int projectId)
    {
        var entities = await _context.ProjectTranslations
            .Where(t => t.ProjectId == projectId)
            .OrderBy(t => t.Language)
            .ToListAsync();

        return ProjectTranslationMapper.ToModelList(entities);
    }

    public async Task<ProjectTranslation?> GetTranslationByIdAsync(int id)
    {
        var entity = await _context.ProjectTranslations.FindAsync(id);
        return entity != null ? ProjectTranslationMapper.ToModel(entity) : null;
    }

    public async Task<ProjectTranslation?> GetTranslationByProjectIdAndLanguageAsync(int projectId, string language)
    {
        var entity = await _context.ProjectTranslations
            .FirstOrDefaultAsync(t => t.ProjectId == projectId && t.Language == language);

        return entity != null ? ProjectTranslationMapper.ToModel(entity) : null;
    }

    public async Task<ProjectTranslation> CreateTranslationAsync(ProjectTranslation translation)
    {
        var entity = ProjectTranslationMapper.ToEntity(translation);
        _context.ProjectTranslations.Add(entity);
        await _context.SaveChangesAsync();
        return ProjectTranslationMapper.ToModel(entity);
    }

    public async Task UpdateTranslationAsync(ProjectTranslation translation)
    {
        var entity = ProjectTranslationMapper.ToEntity(translation);
        _context.ProjectTranslations.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTranslationAsync(int id)
    {
        var translation = await _context.ProjectTranslations.FindAsync(id);
        if (translation != null)
        {
            _context.ProjectTranslations.Remove(translation);
            await _context.SaveChangesAsync();
        }
    }
}

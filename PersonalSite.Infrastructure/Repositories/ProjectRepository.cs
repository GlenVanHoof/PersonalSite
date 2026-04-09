using Microsoft.EntityFrameworkCore;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Interfaces;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly PortfolioDbContext _context;

    public ProjectRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync(string? language = null)
    {
        var query = _context.Projects
            .Include(p => p.Translations)
            .OrderBy(p => p.OrderIndex);

        if (!string.IsNullOrEmpty(language))
        {
            return await query
                .Select(p => new ProjectEntity
                {
                    Id = p.Id,
                    Slug = p.Slug,
                    GithubUrl = p.GithubUrl,
                    ImagePath = p.ImagePath,
                    OrderIndex = p.OrderIndex,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    Translations = p.Translations.Where(t => t.Language == language).ToList()
                })
                .ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<ProjectEntity?> GetProjectBySlugAsync(string slug, string? language = null)
    {
        var query = _context.Projects
            .Include(p => p.Translations)
            .Where(p => p.Slug == slug);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<ProjectEntity> CreateProjectAsync(ProjectEntity project)
    {
        project.CreatedAt = DateTime.UtcNow;
        project.UpdatedAt = DateTime.UtcNow;
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task UpdateProjectAsync(ProjectEntity project)
    {
        project.UpdatedAt = DateTime.UtcNow;
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProjectAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
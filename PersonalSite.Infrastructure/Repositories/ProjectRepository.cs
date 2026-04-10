using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Models;
using PersonalSite.Core.Interfaces;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly PortfolioDbContext _context;

    public ProjectRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync(string? language = null)
    {
        var query = _context.Projects
            .Include(p => p.Translations)
            .OrderBy(p => p.OrderIndex);

        if (!string.IsNullOrEmpty(language))
        {
            var entities = await query
                .Select(p => new
                {
                    p.Id,
                    p.Slug,
                    p.GithubUrl,
                    p.ImagePath,
                    p.OrderIndex,
                    p.CreatedAt,
                    p.UpdatedAt,
                    Translations = p.Translations.Where(t => t.Language == language).ToList()
                })
                .ToListAsync();

            return entities.Select(e => ProjectMapper.ToModel(new Models.ProjectEntity
            {
                Id = e.Id,
                Slug = e.Slug,
                GithubUrl = e.GithubUrl,
                ImagePath = e.ImagePath,
                OrderIndex = e.OrderIndex,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt,
                Translations = e.Translations
            }));
        }

        var allEntities = await query.ToListAsync();
        return ProjectMapper.ToModelList(allEntities);
    }

    public async Task<Project?> GetProjectBySlugAsync(string slug, string? language = null)
    {
        var query = _context.Projects
            .Include(p => p.Translations)
            .Where(p => p.Slug == slug);

        var entity = await query.FirstOrDefaultAsync();
        return entity != null ? ProjectMapper.ToModel(entity) : null;
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        var entity = ProjectMapper.ToEntity(project);
        _context.Projects.Add(entity);
        await _context.SaveChangesAsync();
        return ProjectMapper.ToModel(entity);
    }

    public async Task UpdateProjectAsync(Project project)
    {
        var entity = ProjectMapper.ToEntity(project);
        _context.Projects.Update(entity);
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
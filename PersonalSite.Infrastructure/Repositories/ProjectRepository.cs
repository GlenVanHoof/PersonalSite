using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly PortfolioDbContext _context;
    private readonly TranslationHelper _translationHelper;

    public ProjectRepository(PortfolioDbContext context)
    {
        _context = context;
        _translationHelper = new TranslationHelper(context);
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var entities = await _context.Projects
            .Include(p => p.Pictures)
            .OrderBy(p => p.OrderIndex)
            .ToListAsync();

        var projects = new List<Project>();
        foreach (var entity in entities)
        {
            projects.Add(await ProjectMapper.ToDomainAsync(entity, _translationHelper));
        }

        return projects;
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        var entity = await _context.Projects
            .Include(p => p.Pictures)
            .FirstOrDefaultAsync(p => p.Id == id);

        return entity == null ? null : await ProjectMapper.ToDomainAsync(entity, _translationHelper);
    }

    public async Task<Project?> GetProjectBySlugAsync(string slug)
    {
        var entity = await _context.Projects
            .Include(p => p.Pictures)
            .FirstOrDefaultAsync(p => p.Slug == slug);

        return entity == null ? null : await ProjectMapper.ToDomainAsync(entity, _translationHelper);
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        var entity = ProjectMapper.ToEntity(project);
        
        _context.Projects.Add(entity);
        await _context.SaveChangesAsync();

        // Save translations
        var translations = ProjectMapper.ExtractTranslations(project);
        await _translationHelper.SaveAllTranslationsAsync("Project", entity.Id, translations);

        return await GetProjectByIdAsync(entity.Id) ?? project;
    }

    public async Task UpdateProjectAsync(Project project)
    {
        var entity = await _context.Projects.FindAsync(project.Id);
        if (entity == null)
            throw new KeyNotFoundException($"Project with ID {project.Id} not found.");

        // Update basic properties
        entity.Slug = project.Slug;
        entity.GithubUrl = project.GithubUrl;
        entity.ImagePath = project.ImagePath;
        entity.OrderIndex = project.OrderIndex;

        _context.Projects.Update(entity);
        await _context.SaveChangesAsync();

        // Update translations
        var translations = ProjectMapper.ExtractTranslations(project);
        await _translationHelper.SaveAllTranslationsAsync("Project", entity.Id, translations);
    }

    public async Task DeleteProjectAsync(int id)
    {
        var entity = await _context.Projects.FindAsync(id);
        if (entity != null)
        {
            // Delete translations first
            await _translationHelper.DeleteTranslationsAsync("Project", id);
            
            _context.Projects.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
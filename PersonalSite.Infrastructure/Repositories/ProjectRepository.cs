using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly PortfolioDbContext _context;
    private readonly TranslationHelper _translationHelper;

    public ProjectRepository(PortfolioDbContext context, TranslationHelper translationHelper)
    {
        _context = context;
        _translationHelper = translationHelper;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var entities = await _context.Projects
            .Include(p => p.Pictures)
            .Include(p => p.Skills)
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
            .Include(p => p.Skills)
            .FirstOrDefaultAsync(p => p.Id == id);

        return entity == null ? null : await ProjectMapper.ToDomainAsync(entity, _translationHelper);
    }

    public async Task<Project?> GetProjectBySlugAsync(string slug)
    {
        var entity = await _context.Projects
            .Include(p => p.Pictures)
            .Include(p => p.Skills)
            .FirstOrDefaultAsync(p => p.Slug == slug);

        return entity == null ? null : await ProjectMapper.ToDomainAsync(entity, _translationHelper);
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        var entity = ProjectMapper.ToEntity(project);

        _context.Projects.Add(entity);
        await _context.SaveChangesAsync();

        // Attach existing skills via tracked entities
        if (project.Skills?.Any() == true)
        {
            var skillIds = project.Skills.Select(s => s.Id).ToList();
            var trackedSkills = await _context.Skills
                .Where(s => skillIds.Contains(s.Id))
                .ToListAsync();
            foreach (var skill in trackedSkills)
                entity.Skills.Add(skill);
            await _context.SaveChangesAsync();
        }

        // Save translations
        var translations = ProjectMapper.ExtractTranslations(project);
        await _translationHelper.SaveAllTranslationsAsync("Project", entity.Id, translations);

        return await GetProjectByIdAsync(entity.Id) ?? project;
    }

    public async Task UpdateProjectAsync(Project project)
    {
        var entity = await _context.Projects
            .Include(p => p.Skills)
            .FirstOrDefaultAsync(p => p.Id == project.Id);
        if (entity == null)
            throw new KeyNotFoundException($"Project with ID {project.Id} not found.");

        // Update basic properties
        entity.Slug = project.Slug;
        entity.GithubUrl = project.GithubUrl;
        entity.ProjectUrl = project.ProjectUrl;
        entity.ImagePath = project.ImagePath;
        entity.OrderIndex = project.OrderIndex;
        entity.UpdatedOn = project.UpdatedOn;

        // Update skills: clear and re-attach tracked entities
        entity.Skills.Clear();
        if (project.Skills?.Any() == true)
        {
            var skillIds = project.Skills.Select(s => s.Id).ToList();
            var trackedSkills = await _context.Skills
                .Where(s => skillIds.Contains(s.Id))
                .ToListAsync();
            foreach (var skill in trackedSkills)
                entity.Skills.Add(skill);
        }

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
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;

namespace PersonalSite.Core.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllProjectsAsync();
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        return await _projectRepository.GetProjectByIdAsync(id);
    }

    public async Task<Project?> GetProjectBySlugAsync(string slug)
    {
        return await _projectRepository.GetProjectBySlugAsync(slug);
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        return await _projectRepository.CreateProjectAsync(project);
    }

    public async Task UpdateProjectAsync(Project project)
    {
        await _projectRepository.UpdateProjectAsync(project);
    }

    public async Task DeleteProjectAsync(int id)
    {
        await _projectRepository.DeleteProjectAsync(id);
    }

    // Additional service methods
    public async Task<IEnumerable<Project>> GetProjectsOrderedAsync()
    {
        var projects = await _projectRepository.GetAllProjectsAsync();
        return projects.OrderBy(p => p.OrderIndex);
    }

    public async Task<Project?> GetProjectWithPicturesAsync(int id)
    {
        return await _projectRepository.GetProjectByIdAsync(id);
    }
}

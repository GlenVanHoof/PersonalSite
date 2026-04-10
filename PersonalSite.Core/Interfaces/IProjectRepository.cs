using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllProjectsAsync(string? language = null);
    Task<Project?> GetProjectBySlugAsync(string slug, string? language = null);
    Task<Project> CreateProjectAsync(Project project);
    Task UpdateProjectAsync(Project project);
    Task DeleteProjectAsync(int id);
}
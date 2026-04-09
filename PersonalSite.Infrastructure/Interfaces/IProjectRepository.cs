using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync(string? language = null);
    Task<ProjectEntity?> GetProjectBySlugAsync(string slug, string? language = null);
    Task<ProjectEntity> CreateProjectAsync(ProjectEntity project);
    Task UpdateProjectAsync(ProjectEntity project);
    Task DeleteProjectAsync(int id);
}
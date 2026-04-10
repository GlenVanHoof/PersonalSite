using System;
using System.Collections.Generic;
using System.Text;
using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync(string? language = null);
        Task<Project?> GetProjectBySlugAsync(string slug, string? language = null);
        Task<Project> CreateProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int id);
    }
}

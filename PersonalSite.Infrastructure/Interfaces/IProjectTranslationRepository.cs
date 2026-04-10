using PersonalSite.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalSite.Infrastructure.Interfaces
{
    public interface IProjectTranslationRepository
    {
        Task<IEnumerable<ProjectTranslationEntity>> GetAllProjectTranslationsAsync();
        Task<ProjectTranslationEntity?> GetProjectTranslationByIdAsync(int id);
        Task<ProjectTranslationEntity?> GetProjectTranslationByProjectIdAndLanguageAsync(int id, string language);
        Task<ProjectEntity> CreateProjectTranslationAsync(ProjectTranslationEntity projectTranslation);
        Task UpdateProjectAsync(ProjectTranslationEntity projectTranslation);
        Task DeleteProjectAsync(int id);

    }
}

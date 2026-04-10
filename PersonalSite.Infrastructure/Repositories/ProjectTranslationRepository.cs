using Microsoft.EntityFrameworkCore;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Interfaces;
using PersonalSite.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalSite.Infrastructure.Repositories
{
    public class ProjectTranslationRepository : IProjectTranslationRepository
    {
        private readonly PortfolioDbContext _context;

        public ProjectTranslationRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectTranslationEntity>> GetAllProjectTranslationsAsync()
        {
            return await _context.ProjectTranslations
                .Include(pt => pt.Project)
                .ToListAsync();
        }

        public async Task<ProjectTranslationEntity?> GetProjectTranslationByIdAsync(int id)
        {
            return await _context.ProjectTranslations
                .Include(pt => pt.Project)
                .FirstOrDefaultAsync(pt => pt.Id == id);
        }

        public async Task<ProjectTranslationEntity?> GetProjectTranslationByProjectIdAndLanguageAsync(int id, string language)
        {
            return await _context.ProjectTranslations
                .Include(pt => pt.Project)
                .FirstOrDefaultAsync(pt => pt.ProjectId == id && pt.Language == language);
        }

        public async Task<ProjectEntity> CreateProjectTranslationAsync(ProjectTranslationEntity projectTranslation)
        {
            _context.ProjectTranslations.Add(projectTranslation);
            await _context.SaveChangesAsync();
            return projectTranslation.Project;
        }

        public async Task UpdateProjectAsync(ProjectTranslationEntity projectTranslation)
        {
            _context.ProjectTranslations.Update(projectTranslation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var projectTranslation = await _context.ProjectTranslations.FindAsync(id);
            if (projectTranslation != null)
            {
                _context.ProjectTranslations.Remove(projectTranslation);
                await _context.SaveChangesAsync();
            }
        }
    }
}

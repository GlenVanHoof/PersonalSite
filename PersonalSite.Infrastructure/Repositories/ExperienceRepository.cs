using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class ExperienceRepository : IExperienceRepository
{
    private readonly PortfolioDbContext _context;
    private readonly TranslationHelper _translationHelper;

    public ExperienceRepository(PortfolioDbContext context, TranslationHelper translationHelper)
    {
        _context = context;
        _translationHelper = translationHelper;
    }

    public async Task<IEnumerable<Experience>> GetAllExperiencesAsync()
    {
        var entities = await _context.Experiences.OrderByDescending(e => e.StartDate).ToListAsync();
        var experiences = new List<Experience>();

        foreach (var entity in entities)
        {
            experiences.Add(await ExperienceMapper.ToDomainAsync(entity, _translationHelper));
        }

        return experiences;
    }

    public async Task<Experience?> GetExperienceByIdAsync(int id)
    {
        var entity = await _context.Experiences.FindAsync(id);
        return entity == null ? null : await ExperienceMapper.ToDomainAsync(entity, _translationHelper);
    }

    public async Task<Experience> CreateExperienceAsync(Experience experience)
    {
        var entity = ExperienceMapper.ToEntity(experience);
        _context.Experiences.Add(entity);
        await _context.SaveChangesAsync();

        var translations = ExperienceMapper.ExtractTranslations(experience);
        await _translationHelper.SaveAllTranslationsAsync("Experience", entity.Id, translations);

        return await GetExperienceByIdAsync(entity.Id) ?? experience;
    }

    public async Task UpdateExperienceAsync(Experience experience)
    {
        var entity = await _context.Experiences.FindAsync(experience.Id);
        if (entity == null)
            throw new KeyNotFoundException($"Experience with ID {experience.Id} not found.");

        entity.StartDate = experience.StartDate;
        entity.EndDate = experience.EndDate;

        _context.Experiences.Update(entity);
        await _context.SaveChangesAsync();

        var translations = ExperienceMapper.ExtractTranslations(experience);
        await _translationHelper.SaveAllTranslationsAsync("Experience", entity.Id, translations);
    }

    public async Task DeleteExperienceAsync(int id)
    {
        var entity = await _context.Experiences.FindAsync(id);
        if (entity != null)
        {
            await _translationHelper.DeleteTranslationsAsync("Experience", id);
            _context.Experiences.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
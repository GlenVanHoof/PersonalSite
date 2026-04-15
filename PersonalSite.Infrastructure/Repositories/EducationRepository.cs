using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class EducationRepository : IEducationRepository
{
    private readonly PortfolioDbContext _context;
    private readonly TranslationHelper _translationHelper;

    public EducationRepository(PortfolioDbContext context)
    {
        _context = context;
        _translationHelper = new TranslationHelper(context);
    }

    public async Task<IEnumerable<Education>> GetAllEducationsAsync()
    {
        var entities = await _context.Educations.OrderByDescending(e => e.StartDate).ToListAsync();
        var educations = new List<Education>();
        
        foreach (var entity in entities)
        {
            educations.Add(await EducationMapper.ToDomainAsync(entity, _translationHelper));
        }
        
        return educations;
    }

    public async Task<Education?> GetEducationByIdAsync(int id)
    {
        var entity = await _context.Educations.FindAsync(id);
        return entity == null ? null : await EducationMapper.ToDomainAsync(entity, _translationHelper);
    }

    public async Task<Education> CreateEducationAsync(Education education)
    {
        var entity = EducationMapper.ToEntity(education);
        _context.Educations.Add(entity);
        await _context.SaveChangesAsync();

        var translations = EducationMapper.ExtractTranslations(education);
        await _translationHelper.SaveAllTranslationsAsync("Education", entity.Id, translations);

        return await GetEducationByIdAsync(entity.Id) ?? education;
    }

    public async Task UpdateEducationAsync(Education education)
    {
        var entity = await _context.Educations.FindAsync(education.Id);
        if (entity == null)
            throw new KeyNotFoundException($"Education with ID {education.Id} not found.");

        entity.StartDate = education.StartDate;
        entity.EndDate = education.EndDate;

        _context.Educations.Update(entity);
        await _context.SaveChangesAsync();

        var translations = EducationMapper.ExtractTranslations(education);
        await _translationHelper.SaveAllTranslationsAsync("Education", entity.Id, translations);
    }

    public async Task DeleteEducationAsync(int id)
    {
        var entity = await _context.Educations.FindAsync(id);
        if (entity != null)
        {
            await _translationHelper.DeleteTranslationsAsync("Education", id);
            _context.Educations.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
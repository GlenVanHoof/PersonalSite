using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class PictureRepository : IPictureRepository
{
    private readonly PortfolioDbContext _context;

    public PictureRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Picture>> GetAllAsync()
    {
        var entities = await _context.Pictures
            .OrderByDescending(p => p.CreatedOn)
            .ToListAsync();

        return entities.Select(PictureMapper.ToDomain);
    }

    public async Task<Picture?> GetByIdAsync(int id)
    {
        var entity = await _context.Pictures.FindAsync(id);
        return entity == null ? null : PictureMapper.ToDomain(entity);
    }

    public async Task<IEnumerable<Picture>> GetByProjectIdAsync(int projectId)
    {
        var entities = await _context.Pictures
            .Where(p => p.ProjectId == projectId)
            .OrderBy(p => p.Id)
            .ToListAsync();

        return entities.Select(PictureMapper.ToDomain);
    }

    public async Task<Picture> CreateAsync(Picture picture)
    {
        var entity = PictureMapper.ToEntity(picture);
        _context.Pictures.Add(entity);
        await _context.SaveChangesAsync();
        return PictureMapper.ToDomain(entity);
    }

    public async Task<Picture> UpdateAsync(Picture picture)
    {
        var entity = PictureMapper.ToEntity(picture);
        _context.Pictures.Update(entity);
        await _context.SaveChangesAsync();
        return PictureMapper.ToDomain(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Pictures.FindAsync(id);
        if (entity != null)
        {
            _context.Pictures.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
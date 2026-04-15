using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class GalleryPictureRepository : IGalleryPictureRepository
{
    private readonly PortfolioDbContext _context;

    public GalleryPictureRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GalleryPicture>> GetAllGalleryPicturesAsync()
    {
        var entities = await _context.GalleryPictures
            .Include(gp => gp.Picture)
            .OrderBy(gp => gp.Position)
            .ToListAsync();

        return entities.Select(GalleryPictureMapper.ToDomain);
    }

    public async Task<GalleryPicture?> GetGalleryPictureByIdAsync(int id)
    {
        var entity = await _context.GalleryPictures
            .Include(gp => gp.Picture)
            .FirstOrDefaultAsync(gp => gp.Id == id);

        return entity == null ? null : GalleryPictureMapper.ToDomain(entity);
    }

    public async Task<GalleryPicture> CreateGalleryPictureAsync(GalleryPicture galleryPicture)
    {
        var entity = GalleryPictureMapper.ToEntity(galleryPicture);
        _context.GalleryPictures.Add(entity);
        await _context.SaveChangesAsync();

        return await GetGalleryPictureByIdAsync(entity.Id) ?? galleryPicture;
    }

    public async Task UpdateGalleryPictureAsync(GalleryPicture galleryPicture)
    {
        var entity = await _context.GalleryPictures.FindAsync(galleryPicture.Id);
        if (entity == null)
            throw new KeyNotFoundException($"GalleryPicture with ID {galleryPicture.Id} not found.");

        entity.Position = galleryPicture.Position;
        entity.PictureId = galleryPicture.PictureId;

        _context.GalleryPictures.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGalleryPictureAsync(int id)
    {
        var entity = await _context.GalleryPictures.FindAsync(id);
        if (entity != null)
        {
            _context.GalleryPictures.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
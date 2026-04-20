using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Core.Models;
using System.Data;

namespace PersonalSite.Core.Services;

public class GalleryPictureService : IGalleryPictureService
{
    private readonly IGalleryPictureRepository _galleryPictureRepository;

    public GalleryPictureService(IGalleryPictureRepository galleryPictureRepository)
    {
        _galleryPictureRepository = galleryPictureRepository;
    }

    public async Task<IEnumerable<GalleryPicture>> GetAllGalleryPicturesAsync()
    {
        return await _galleryPictureRepository.GetAllGalleryPicturesAsync();
    }

    public async Task<IEnumerable<GalleryPicture>> GetGalleryPicturesOrderedAsync()
    {
        var pictures = await _galleryPictureRepository.GetAllGalleryPicturesAsync();
        return pictures.OrderBy(p => p.Position);
    }

    public async Task<GalleryPicture?> GetGalleryPictureByIdAsync(int id)
    {
        return await _galleryPictureRepository.GetGalleryPictureByIdAsync(id);
    }

    public async Task<GalleryPicture> CreateGalleryPictureAsync(GalleryPicture galleryPicture)
    {
        galleryPicture.Position = (await _galleryPictureRepository.GetGalleryPictureCountAsync()) + 1;
        return await _galleryPictureRepository.CreateGalleryPictureAsync(galleryPicture);
    }

    public async Task UpdateGalleryPictureAsync(GalleryPicture galleryPicture)
    {
        await _galleryPictureRepository.UpdateGalleryPictureAsync(galleryPicture);
    }

    public async Task DeleteGalleryPictureAsync(int id)
    {
        await _galleryPictureRepository.DeleteGalleryPictureAsync(id);
    }

    public async Task ReorderGalleryPicturesAsync(int id, string direction, int amount = 1)
    {
        if (direction.ToLower() != "up" && direction.ToLower() != "down")
            throw new ArgumentException("Direction must be either 'up' or 'down'.");

        var picture = await _galleryPictureRepository.GetGalleryPictureByIdAsync(id);
        if (picture == null)
            throw new KeyNotFoundException($"GalleryPicture with id = {id} not found.");
        var allPictures = await _galleryPictureRepository.GetAllGalleryPicturesAsync();

        int currentPosition = picture.Position;
        int newPosition = direction.ToLower() == "up" ? currentPosition - amount : currentPosition + amount;
        if (newPosition < 1)
            newPosition = 1;
        if (newPosition > allPictures.Count())
            newPosition = allPictures.Count();

        // Update the positions of the affected pictures
        if (newPosition != currentPosition)
        {
            var affectedPictures = allPictures
                .Where(p => p.Position >= Math.Min(currentPosition, newPosition)
                         && p.Position <= Math.Max(currentPosition, newPosition));

            foreach (var affectedPicture in affectedPictures)
            {
                affectedPicture.Position += (newPosition > currentPosition) ? -1 : 1;
                await _galleryPictureRepository.UpdateGalleryPictureAsync(affectedPicture);
            }
        }

        picture.Position = newPosition;
        await _galleryPictureRepository.UpdateGalleryPictureAsync(picture);
    }
}
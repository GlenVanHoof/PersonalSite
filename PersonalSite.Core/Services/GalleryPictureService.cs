using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Core.Models;

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

    public async Task ReorderGalleryPicturesAsync(List<int> orderedIds)
    {
        var pictures = await _galleryPictureRepository.GetAllGalleryPicturesAsync();
        var pictureDict = pictures.ToDictionary(p => p.Id);

        for (int i = 0; i < orderedIds.Count; i++)
        {
            if (pictureDict.TryGetValue(orderedIds[i], out var picture))
            {
                picture.Position = i;
                await _galleryPictureRepository.UpdateGalleryPictureAsync(picture);
            }
        }
    }
}
using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces;

public interface IGalleryPictureRepository
{
    Task<IEnumerable<GalleryPicture>> GetAllGalleryPicturesAsync();
    Task<GalleryPicture?> GetGalleryPictureByIdAsync(int id);
    Task<GalleryPicture> CreateGalleryPictureAsync(GalleryPicture galleryPicture);
    Task UpdateGalleryPictureAsync(GalleryPicture galleryPicture);
    Task DeleteGalleryPictureAsync(int id);
}
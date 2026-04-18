using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces.Repositories;

public interface IGalleryPictureRepository
{
    Task<IEnumerable<GalleryPicture>> GetAllGalleryPicturesAsync();
    Task<GalleryPicture?> GetGalleryPictureByIdAsync(int id);
    Task<GalleryPicture?> GetGalleryPictureByPositionAsync(int position);
    Task<GalleryPicture> CreateGalleryPictureAsync(GalleryPicture galleryPicture);
    Task UpdateGalleryPictureAsync(GalleryPicture galleryPicture);
    Task DeleteGalleryPictureAsync(int id);
    Task<int> GetGalleryPictureCountAsync();
}
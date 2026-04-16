using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces.Services;

public interface IGalleryPictureService
{
    Task<IEnumerable<GalleryPicture>> GetAllGalleryPicturesAsync();
    Task<IEnumerable<GalleryPicture>> GetGalleryPicturesOrderedAsync();
    Task<GalleryPicture?> GetGalleryPictureByIdAsync(int id);
    Task<GalleryPicture> CreateGalleryPictureAsync(GalleryPicture galleryPicture);
    Task UpdateGalleryPictureAsync(GalleryPicture galleryPicture);
    Task DeleteGalleryPictureAsync(int id);
    Task ReorderGalleryPicturesAsync(List<int> orderedIds); // For drag & drop reordering
}
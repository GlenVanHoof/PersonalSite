using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces;

public interface IPictureService
{
    Task<IEnumerable<Picture>> GetAllPicturesAsync();
    Task<Picture?> GetPictureByIdAsync(int id);
    Task<IEnumerable<Picture>> GetPicturesByProjectIdAsync(int projectId);
    Task<Picture> CreatePictureAsync(Picture picture);
    Task<Picture> UpdatePictureAsync(Picture picture);
    Task DeletePictureAsync(int id);
}
using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces.Repositories;

public interface IPictureRepository
{
    Task<IEnumerable<Picture>> GetAllAsync();
    Task<Picture?> GetByIdAsync(int id);
    Task<IEnumerable<Picture>> GetByProjectIdAsync(int projectId);
    Task<Picture> CreateAsync(Picture picture);
    Task<Picture> UpdateAsync(Picture picture);
    Task DeleteAsync(int id);
}
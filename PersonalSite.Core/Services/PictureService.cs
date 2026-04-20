using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Core.Models;

namespace PersonalSite.Core.Services;

public class PictureService : IPictureService
{
    private readonly IPictureRepository _pictureRepository;

    public PictureService(IPictureRepository pictureRepository)
    {
        _pictureRepository = pictureRepository;
    }

    public async Task<IEnumerable<Picture>> GetAllPicturesAsync()
    {
        return await _pictureRepository.GetAllAsync();
    }

    public async Task<Picture?> GetPictureByIdAsync(int id)
    {
        return await _pictureRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Picture>> GetPicturesByProjectIdAsync(int projectId)
    {
        return await _pictureRepository.GetByProjectIdAsync(projectId);
    }

    public async Task<Picture> CreatePictureAsync(Picture picture)
    {
        return await _pictureRepository.CreateAsync(picture);
    }

    public async Task<Picture> UpdatePictureAsync(Picture picture)
    {
        return await _pictureRepository.UpdateAsync(picture);
    }

    public async Task DeletePictureAsync(int id)
    {
        await _pictureRepository.DeleteAsync(id);
    }
}
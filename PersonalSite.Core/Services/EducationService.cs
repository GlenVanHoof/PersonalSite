using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Core.Models;

namespace PersonalSite.Core.Services;

public class EducationService : IEducationService
{
    private readonly IEducationRepository _educationRepository;

    public EducationService(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }

    public async Task<IEnumerable<Education>> GetAllEducationsAsync()
    {
        return await _educationRepository.GetAllEducationsAsync();
    }

    public async Task<Education?> GetEducationByIdAsync(int id)
    {
        return await _educationRepository.GetEducationByIdAsync(id);
    }

    public async Task<Education> CreateEducationAsync(Education education)
    {
        return await _educationRepository.CreateEducationAsync(education);
    }

    public async Task UpdateEducationAsync(Education education)
    {
        await _educationRepository.UpdateEducationAsync(education);
    }

    public async Task DeleteEducationAsync(int id)
    {
        await _educationRepository.DeleteEducationAsync(id);
    }

    public async Task<IEnumerable<Education>> GetEducationsOrderedByDateAsync()
    {
        var educations = await _educationRepository.GetAllEducationsAsync();
        return educations.OrderByDescending(e => e.StartDate);
    }
}
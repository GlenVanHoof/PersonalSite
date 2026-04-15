using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces;

public interface ILanguageRepository
{
    Task<IEnumerable<Language>> GetAllLanguagesAsync();
    Task<Language?> GetLanguageByIdAsync(int id);
    Task<Language?> GetLanguageByCodeAsync(string code);
}
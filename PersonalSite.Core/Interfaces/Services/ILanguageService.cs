using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces.Services;

public interface ILanguageService
{
    Task<IEnumerable<Language>> GetAllLanguagesAsync();
    Task<Language?> GetLanguageByIdAsync(int id);
    Task<Language?> GetLanguageByCodeAsync(string code);
    Task<Language> GetCurrentLanguageAsync(); // Based on current culture
}
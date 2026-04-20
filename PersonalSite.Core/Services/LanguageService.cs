using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Core.Models;
using System.Globalization;

namespace PersonalSite.Core.Services;

public class LanguageService : ILanguageService
{
    private readonly ILanguageRepository _languageRepository;

    public LanguageService(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
    {
        return await _languageRepository.GetAllLanguagesAsync();
    }

    public async Task<Language?> GetLanguageByIdAsync(int id)
    {
        return await _languageRepository.GetLanguageByIdAsync(id);
    }

    public async Task<Language?> GetLanguageByCodeAsync(string code)
    {
        return await _languageRepository.GetLanguageByCodeAsync(code);
    }

    public async Task<Language> GetCurrentLanguageAsync()
    {
        var currentCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        var language = await _languageRepository.GetLanguageByCodeAsync(currentCulture);
        
        // Fallback to English if current language not found
        return language ?? await _languageRepository.GetLanguageByCodeAsync("en") 
            ?? throw new InvalidOperationException("Default language 'en' not found in database.");
    }
}
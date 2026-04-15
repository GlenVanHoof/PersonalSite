using PersonalSite.Core.Interfaces;
using System.Globalization;

namespace PersonalSite.Core.Services;

public class CurrentLanguageService : ICurrentLanguageService
{
    /// <summary>
    /// Gets the current language code (e.g., "en", "nl", "fr", "de")
    /// </summary>
    public string GetCurrentLanguageCode()
    {
        var culture = CultureInfo.CurrentCulture;
        return culture.TwoLetterISOLanguageName;
    }

    /// <summary>
    /// Gets the current culture name (e.g., "en-US", "nl-NL")
    /// </summary>
    public string GetCurrentCultureName()
    {
        return CultureInfo.CurrentCulture.Name;
    }

    /// <summary>
    /// Gets translation from dictionary for current language, with fallback to first available
    /// </summary>
    public string GetTranslation(Dictionary<string, string>? translations)
    {
        if (translations == null || translations.Count == 0)
            return string.Empty;

        var currentLang = GetCurrentLanguageCode();
        
        // Try to get translation for current language
        if (translations.TryGetValue(currentLang, out var translation))
            return translation;

        // Fallback to English
        if (translations.TryGetValue("en", out var englishTranslation))
            return englishTranslation;

        // Fallback to first available translation
        return translations.Values.FirstOrDefault() ?? string.Empty;
    }

    /// <summary>
    /// Gets all translations for a specific language code
    /// </summary>
    public Dictionary<string, string> GetTranslationsForLanguage(
        Dictionary<string, Dictionary<string, string>> allTranslations, 
        string? languageCode = null)
    {
        var lang = languageCode ?? GetCurrentLanguageCode();
        var result = new Dictionary<string, string>();

        foreach (var (fieldName, translations) in allTranslations)
        {
            if (translations.TryGetValue(lang, out var translation))
            {
                result[fieldName] = translation;
            }
            else if (translations.TryGetValue("en", out var fallback))
            {
                result[fieldName] = fallback;
            }
        }

        return result;
    }
}

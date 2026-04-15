namespace PersonalSite.Core.Interfaces;

public interface ICurrentLanguageService
{
    /// <summary>
    /// Gets the current language code (e.g., "en", "nl", "fr", "de")
    /// </summary>
    string GetCurrentLanguageCode();

    /// <summary>
    /// Gets the current culture name (e.g., "en-US", "nl-NL")
    /// </summary>
    string GetCurrentCultureName();

    /// <summary>
    /// Gets translation from dictionary for current language, with fallback to first available
    /// </summary>
    string GetTranslation(Dictionary<string, string>? translations);

    /// <summary>
    /// Gets all translations for a specific language code
    /// </summary>
    Dictionary<string, string> GetTranslationsForLanguage(
        Dictionary<string, Dictionary<string, string>> allTranslations, 
        string? languageCode = null);
}
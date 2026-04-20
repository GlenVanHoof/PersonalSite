using Microsoft.EntityFrameworkCore;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public class TranslationHelper
{
    private readonly PortfolioDbContext _context;

    public TranslationHelper(PortfolioDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get translations for a specific entity field - returns Dictionary<languageCode, text>
    /// </summary>
    public async Task<Dictionary<string, string>> GetTranslationsAsync(string contentType, int referenceId, string fieldName)
    {
        var translations = await _context.ContentTranslations
            .Include(t => t.ContentField)
                .ThenInclude(f => f.ContentItem)
            .Include(t => t.Language)
            .Where(t => 
                t.ContentField.ContentItem.ContentType == contentType &&
                t.ContentField.ContentItem.ReferenceId == referenceId &&
                t.ContentField.FieldName == fieldName)
            .ToDictionaryAsync(
                t => t.Language.Code,
                t => t.Text);

        return translations;
    }

    /// <summary>
    /// Get all translations for an entity - returns Dictionary<fieldName, Dictionary<languageCode, text>>
    /// </summary>
    public async Task<Dictionary<string, Dictionary<string, string>>> GetAllTranslationsAsync(string contentType, int referenceId)
    {
        var contentItem = await _context.ContentItems
            .Include(ci => ci.ContentFields)
                .ThenInclude(cf => cf.Translations)
                    .ThenInclude(t => t.Language)
            .FirstOrDefaultAsync(ci => ci.ContentType == contentType && ci.ReferenceId == referenceId);

        if (contentItem == null)
            return new Dictionary<string, Dictionary<string, string>>();

        var result = new Dictionary<string, Dictionary<string, string>>();

        foreach (var field in contentItem.ContentFields)
        {
            result[field.FieldName] = field.Translations.ToDictionary(
                t => t.Language.Code,
                t => t.Text
            );
        }

        return result;
    }

    /// <summary>
    /// Save translations for a specific field
    /// </summary>
    public async Task SaveTranslationsAsync(string contentType, int referenceId, string fieldName, Dictionary<string, string> translations)
    {
        // Find or create ContentItem
        var contentItem = await _context.ContentItems
            .FirstOrDefaultAsync(ci => ci.ContentType == contentType && ci.ReferenceId == referenceId);

        if (contentItem == null)
        {
            contentItem = new ContentItemEntity
            {
                ContentType = contentType,
                ReferenceId = referenceId
            };
            _context.ContentItems.Add(contentItem);
            await _context.SaveChangesAsync();
        }

        // Find or create ContentField
        var contentField = await _context.ContentFields
            .Include(cf => cf.Translations)
            .FirstOrDefaultAsync(cf => cf.ContentItemId == contentItem.Id && cf.FieldName == fieldName);

        if (contentField == null)
        {
            contentField = new ContentFieldEntity
            {
                ContentItemId = contentItem.Id,
                FieldName = fieldName,
                ContentItem = contentItem
            };
            _context.ContentFields.Add(contentField);
            await _context.SaveChangesAsync();
        }

        // Get all languages
        var languages = await _context.Languages.ToDictionaryAsync(l => l.Code, l => l);

        // Save/Update translations
        foreach (var (languageCode, text) in translations)
        {
            if (!languages.ContainsKey(languageCode))
                continue;

            var language = languages[languageCode];
            var translation = contentField.Translations.FirstOrDefault(t => t.LanguageId == language.Id);

            if (translation == null)
            {
                translation = new ContentTranslationEntity
                {
                    ContentFieldId = contentField.Id,
                    LanguageId = language.Id,
                    Text = text,
                    ContentField = contentField,
                    Language = language
                };
                _context.ContentTranslations.Add(translation);
            }
            else
            {
                translation.Text = text;
            }
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Save multiple fields at once
    /// </summary>
    public async Task SaveAllTranslationsAsync(string contentType, int referenceId, Dictionary<string, Dictionary<string, string>> fields)
    {
        foreach (var (fieldName, translations) in fields)
        {
            await SaveTranslationsAsync(contentType, referenceId, fieldName, translations);
        }
    }

    /// <summary>
    /// Delete all translations for an entity
    /// </summary>
    public async Task DeleteTranslationsAsync(string contentType, int referenceId)
    {
        var contentItem = await _context.ContentItems
            .FirstOrDefaultAsync(ci => ci.ContentType == contentType && ci.ReferenceId == referenceId);

        if (contentItem != null)
        {
            _context.ContentItems.Remove(contentItem); // Cascade delete
            await _context.SaveChangesAsync();
        }
    }
}
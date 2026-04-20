namespace PersonalSite.Infrastructure.Models;

public class LanguageEntity
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }

    // Navigation properties
    public ICollection<ContentTranslationEntity> Translations { get; set; } = new List<ContentTranslationEntity>();
}
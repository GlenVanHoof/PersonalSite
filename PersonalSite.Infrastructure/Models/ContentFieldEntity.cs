namespace PersonalSite.Infrastructure.Models;

public class ContentFieldEntity
{
    public int Id { get; set; }
    public int ContentItemId { get; set; }
    public required string FieldName { get; set; }

    // Navigation properties
    public required ContentItemEntity ContentItem { get; set; }
    public ICollection<ContentTranslationEntity> Translations { get; set; } = new List<ContentTranslationEntity>();
}
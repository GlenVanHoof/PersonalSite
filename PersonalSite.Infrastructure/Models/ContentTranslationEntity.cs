namespace PersonalSite.Infrastructure.Models;

public class ContentTranslationEntity
{
    public int Id { get; set; }
    public int ContentFieldId { get; set; }
    public int LanguageId { get; set; }
    public required string Text { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Navigation properties
    public required ContentFieldEntity ContentField { get; set; }
    public required LanguageEntity Language { get; set; }
}
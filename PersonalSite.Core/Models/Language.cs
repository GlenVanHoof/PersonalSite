namespace PersonalSite.Core.Models;

public class Language
{
    public int Id { get; set; }
    public required string Code { get; set; } // "en", "nl", "fr", "de"
    public required string Name { get; set; } // "English", "Nederlands", "Français", "Deutsch"
}
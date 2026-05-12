namespace PersonalSite.Core.Models;

public class Project
{
    public int Id { get; set; }
    public required string Slug { get; set; }
    public string? GithubUrl { get; set; }
    public string? ProjectUrl { get; set; }
    public string? ImagePath { get; set; }
    public int OrderIndex { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Multi-language fields (key = language code: "en", "nl", "fr", "de")
    public Dictionary<string, string> Title { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
    public Dictionary<string, string> ShortDescription { get; set; } = new();
    public List<Picture> Pictures { get; set; } = new();
    public List<Skill> Skills { get; set; } = new();
}

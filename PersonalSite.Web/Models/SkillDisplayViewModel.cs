namespace PersonalSite.Web.Models;

public class SkillDisplayViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Type { get; set; } = string.Empty;
    public int ScoreOutOf100 { get; set; }
}
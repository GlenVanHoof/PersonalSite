namespace PersonalSite.Web.Models;

public class ProjectDisplayViewModel
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
    public string? GithubUrl { get; set; }
}
namespace PersonalSite.Core.Models;

public class Experience
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Multi-language fields
    public Dictionary<string, string> Company { get; set; } = new();
    public Dictionary<string, string> Position { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
}

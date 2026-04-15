namespace PersonalSite.Core.Models;

public class Education
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Multi-language fields
    public Dictionary<string, string> Institution { get; set; } = new();
    public Dictionary<string, string> Degree { get; set; } = new();
    public Dictionary<string, string> FieldOfStudy { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
}

namespace PersonalSite.Core.Models;

public class Certificate
{
    public int Id { get; set; }
    public DateTime AcquiredOn { get; set; }
    public required string Organisation { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Multi-language fields
    public Dictionary<string, string> Name { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
}

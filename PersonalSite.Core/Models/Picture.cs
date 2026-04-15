namespace PersonalSite.Core.Models;

public class Picture
{
    public int Id { get; set; }
    public required string Source { get; set; }
    public int? ProjectId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}
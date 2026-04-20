using PersonalSite.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSite.Core.Models;

public class Skill
{
    public int Id { get; set; }
    [Column(TypeName = "varchar(20)")]
    public required SkillType Type { get; set; }
    public int ScoreOutOf100 { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Multi-language fields
    public Dictionary<string, string> Name { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
}

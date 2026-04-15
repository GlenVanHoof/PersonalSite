namespace PersonalSite.Core.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public required string Type { get; set; }
        public int ScoreOutOf100 { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        // Multi-language fields
        public Dictionary<string, string> Name { get; set; } = new();
        public Dictionary<string, string> Description { get; set; } = new();
    }
}

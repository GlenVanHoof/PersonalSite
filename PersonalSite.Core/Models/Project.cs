namespace PersonalSite.Core.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Slug { get; set; } // Unique identifier for URL (ex.: my-project)
        public string? GitUrl { get; set; }
        public string? ImagePath { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}

namespace PersonalSite.Core.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateOnly AcquiredOn { get; set; }
        public string? Organisation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

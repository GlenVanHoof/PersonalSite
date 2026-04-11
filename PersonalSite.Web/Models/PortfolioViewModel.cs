namespace PersonalSite.Web.Models
{
    public class PortfolioViewModel
    {
        public List<PortfolioProjectViewModel> Projects { get; set; } = new();
        public string CurrentLanguage { get; set; } = "en";
    }

    public class PortfolioProjectViewModel
    {
        public int Id { get; set; }
        public string? Slug { get; set; }
        public string? GitUrl { get; set; }
        public string? ImagePath { get; set; }
        public int OrderIndex { get; set; }
        
        // Translation fields
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public List<string>? Technologies { get; set; }
    }
}

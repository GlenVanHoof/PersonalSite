using PersonalSite.Core.Models;

namespace PersonalSite.Web.Models
{
    public class AboutViewModel
    {
        public string? PictureURL { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public List<Education> Educations { get; set; } = new List<Education>();
        public List<Experience> Experiences { get; set; } = new List<Experience>();
        public List<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}

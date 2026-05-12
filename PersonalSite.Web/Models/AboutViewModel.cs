using PersonalSite.Core.Models;

namespace PersonalSite.Web.Models
{
    public class AboutViewModel
    {
        public string? PictureURL { get; set; }
        public Dictionary<string, List<SkillDisplayViewModel>> Skills { get; set; } = new();
        public List<EducationDisplayViewModel> Educations { get; set; } = new();
        public List<ExperienceDisplayViewModel> Experiences { get; set; } = new();
        public List<CertificateDisplayViewModel> Certificates { get; set; } = new();
        public string? Description { get; set; }
    }
}

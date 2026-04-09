using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalSite.Infrastructure.Models
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string GithubUrl { get; set; }
        public string ImagePath { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<ProjectTranslationEntity> Translations { get; set; } = new List<ProjectTranslationEntity>();
    }
}
 
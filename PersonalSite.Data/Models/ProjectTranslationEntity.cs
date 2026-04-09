using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalSite.Infrastructure.Models
{
    public class ProjectTranslationEntity

    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public List<string> Technologies { get; set; }

        public ProjectEntity Project { get; set; }
    }

}

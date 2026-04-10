using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalSite.Infrastructure.Models
{
    public class SkillEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // "Technical", "Soft", "Language"
        public int ScoreOutOf100 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

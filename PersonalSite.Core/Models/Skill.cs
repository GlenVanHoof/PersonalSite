using PersonalSite.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalSite.Core.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public SkillType Type { get; set; }
        public int ScoreOutOf100 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

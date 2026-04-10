using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalSite.Core.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

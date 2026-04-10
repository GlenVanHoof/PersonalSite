using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalSite.Core.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

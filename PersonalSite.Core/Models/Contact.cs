using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalSite.Core.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}

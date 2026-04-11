namespace PersonalSite.Infrastructure.Models
{
    public class CertificateEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateOnly AcquiredOn { get; set; }
        public string? Organisation { get; set; }
    }
}

namespace PersonalSite.Web.Models;

public class CertificateDisplayViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Organisation { get; set; }
    public DateTime? AcquiredOn { get; set; }
}
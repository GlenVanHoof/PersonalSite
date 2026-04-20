namespace PersonalSite.Infrastructure.Models;

public class CertificateEntity
{
    public int Id { get; set; }
    public DateTime AcquiredOn { get; set; }
    public required string Organisation { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Navigation properties
    public ICollection<ContentItemEntity> ContentItems { get; set; } = new List<ContentItemEntity>();
}

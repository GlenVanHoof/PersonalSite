using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class CertificateConfiguration : IEntityTypeConfiguration<CertificateEntity>
{
    public void Configure(EntityTypeBuilder<CertificateEntity> builder)
    {
        builder.ToTable("Certificates");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Organisation)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(e => e.AcquiredOn)
            .IsRequired();

        builder.Property(e => e.CreatedOn)
            .IsRequired();

        builder.Property(e => e.UpdatedOn)
            .IsRequired();
        
        builder.HasIndex(e => e.AcquiredOn);
    }
}
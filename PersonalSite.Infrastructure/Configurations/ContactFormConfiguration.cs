using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class ContactFormConfiguration : IEntityTypeConfiguration<ContactFormEntity>
{
    public void Configure(EntityTypeBuilder<ContactFormEntity> builder)
    {
        builder.ToTable("ContactForms");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(e => e.Message)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(e => e.CreatedOn)
            .IsRequired();
        
        builder.HasIndex(e => e.CreatedOn);
    }
}
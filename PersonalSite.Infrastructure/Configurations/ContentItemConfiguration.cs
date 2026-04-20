using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class ContentItemConfiguration : IEntityTypeConfiguration<ContentItemEntity>
{
    public void Configure(EntityTypeBuilder<ContentItemEntity> builder)
    {
        builder.ToTable("ContentItems");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.ContentType)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(e => e.ReferenceId)
            .IsRequired();

        builder.Property(e => e.CreatedOn)
            .IsRequired();

        builder.Property(e => e.UpdatedOn)
            .IsRequired();
        
        builder.HasIndex(e => new { e.ContentType, e.ReferenceId });
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class ContentFieldConfiguration : IEntityTypeConfiguration<ContentFieldEntity>
{
    public void Configure(EntityTypeBuilder<ContentFieldEntity> builder)
    {
        builder.ToTable("ContentFields");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.FieldName)
            .IsRequired()
            .HasMaxLength(100);
        
        // Relationship with ContentItem
        builder.HasOne(e => e.ContentItem)
            .WithMany(c => c.ContentFields)
            .HasForeignKey(e => e.ContentItemId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Unique constraint on ContentItemId + FieldName
        builder.HasIndex(e => new { e.ContentItemId, e.FieldName })
            .IsUnique();
    }
}
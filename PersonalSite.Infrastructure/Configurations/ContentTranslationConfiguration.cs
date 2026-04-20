using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class ContentTranslationConfiguration : IEntityTypeConfiguration<ContentTranslationEntity>
{
    public void Configure(EntityTypeBuilder<ContentTranslationEntity> builder)
    {
        builder.ToTable("ContentTranslations");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Text)
            .IsRequired();

        builder.Property(e => e.CreatedOn)
            .IsRequired();

        builder.Property(e => e.UpdatedOn)
            .IsRequired();
        
        // Relationship with ContentField
        builder.HasOne(e => e.ContentField)
            .WithMany(f => f.Translations)
            .HasForeignKey(e => e.ContentFieldId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Relationship with Language
        builder.HasOne(e => e.Language)
            .WithMany(l => l.Translations)
            .HasForeignKey(e => e.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Unique constraint on ContentFieldId + LanguageId
        builder.HasIndex(e => new { e.ContentFieldId, e.LanguageId })
            .IsUnique();
    }
}
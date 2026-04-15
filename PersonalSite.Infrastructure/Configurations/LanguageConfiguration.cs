using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class LanguageConfiguration : IEntityTypeConfiguration<LanguageEntity>
{
    public void Configure(EntityTypeBuilder<LanguageEntity> builder)
    {
        builder.ToTable("Languages");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(10);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasIndex(e => e.Code)
            .IsUnique();

        // Seed data
        builder.HasData(
            new LanguageEntity { Id = 1, Code = "en", Name = "English" },
            new LanguageEntity { Id = 2, Code = "nl", Name = "Nederlands" },
            new LanguageEntity { Id = 3, Code = "fr", Name = "Français" },
            new LanguageEntity { Id = 4, Code = "de", Name = "Deutsch" }
        );
    }
}
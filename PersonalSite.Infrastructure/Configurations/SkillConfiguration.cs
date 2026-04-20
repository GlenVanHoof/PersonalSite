using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<SkillEntity>
{
    public void Configure(EntityTypeBuilder<SkillEntity> builder)
    {
        builder.ToTable("Skills");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.ScoreOutOf100)
            .IsRequired();

        builder.Property(e => e.CreatedOn)
            .IsRequired();

        builder.Property(e => e.UpdatedOn)
            .IsRequired();
        
        builder.HasIndex(e => e.Type);
    }
}
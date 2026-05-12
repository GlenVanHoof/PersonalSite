using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.ToTable("Projects");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Slug)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(e => e.GithubUrl)
            .HasMaxLength(500);

        builder.Property(e => e.ProjectUrl)
            .HasMaxLength(500);

        builder.Property(e => e.ImagePath)
            .HasMaxLength(500);

        builder.Property(e => e.OrderIndex)
            .IsRequired();

        builder.Property(e => e.CreatedOn)
            .IsRequired();

        builder.Property(e => e.UpdatedOn)
            .IsRequired();

        builder.HasIndex(e => e.Slug)
            .IsUnique();

        builder.HasIndex(e => e.OrderIndex);

        // Configure Skills relationship (one-to-many)
        builder.HasMany(e => e.Skills)
            .WithMany();
    }
}
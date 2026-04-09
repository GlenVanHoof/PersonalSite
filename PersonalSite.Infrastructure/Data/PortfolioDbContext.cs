using Microsoft.EntityFrameworkCore;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Data;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectTranslationEntity> ProjectTranslations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ProjectEntity configuratie
        modelBuilder.Entity<ProjectEntity>(entity =>
        {
            entity.ToTable("Projects");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Slug).IsRequired().HasMaxLength(200);
            entity.Property(e => e.GithubUrl).HasMaxLength(500);
            entity.Property(e => e.ImagePath).HasMaxLength(500);
            entity.HasIndex(e => e.Slug).IsUnique();
            entity.HasIndex(e => e.OrderIndex);

            entity.HasMany(e => e.Translations)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ProjectTranslationEntity configuratie
        modelBuilder.Entity<ProjectTranslationEntity>(entity =>
        {
            entity.ToTable("ProjectTranslations");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Language).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.ShortDescription).HasMaxLength(500);
            entity.Property(e => e.Technologies).HasColumnType("jsonb");

            entity.HasIndex(e => new { e.ProjectId, e.Language }).IsUnique();
        });
    }
}
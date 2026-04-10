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
    public DbSet<SkillEntity> Skills { get; set; }
    public DbSet<EducationEntity> Educations { get; set; }
    public DbSet<ExperienceEntity> Experiences { get; set; }
    public DbSet<CertificateEntity> Certificates { get; set; }
    public DbSet<ContactEntity> Contacts { get; set; }

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

        // SkillEntity configuratie
        modelBuilder.Entity<SkillEntity>(entity =>
        {
            entity.ToTable("Skills");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
            entity.Property(e => e.ScoreOutOf100).IsRequired();
            entity.HasIndex(e => e.Type);
        });

        // EducationEntity configuratie
        modelBuilder.Entity<EducationEntity>(entity =>
        {
            entity.ToTable("Educations");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Institution).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Degree).IsRequired().HasMaxLength(200);
            entity.Property(e => e.FieldOfStudy).IsRequired().HasMaxLength(200);
            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.HasIndex(e => e.StartDate);
        });

        // ExperienceEntity configuratie
        modelBuilder.Entity<ExperienceEntity>(entity =>
        {
            entity.ToTable("Experiences");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Company).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Position).IsRequired().HasMaxLength(200);
            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.HasIndex(e => e.StartDate);
        });

        // CertificateEntity configuratie
        modelBuilder.Entity<CertificateEntity>(entity =>
        {
            entity.ToTable("Certificates");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.AcquiredOn).IsRequired();
            entity.Property(e => e.Organisation).HasMaxLength(200);
            entity.HasIndex(e => e.AcquiredOn);
        });

        // ContactEntity configuratie
        modelBuilder.Entity<ContactEntity>(entity =>
        {
            entity.ToTable("Contacts");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Message).IsRequired().HasMaxLength(5000);
            entity.HasIndex(e => e.CreatedAt);
        });
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity is ProjectEntity project)
            {
                if (entry.State == EntityState.Added)
                {
                    project.CreatedAt = DateTime.UtcNow;
                    project.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    project.UpdatedAt = DateTime.UtcNow;
                }
            }
            else if (entry.Entity is SkillEntity skill)
            {
                if (entry.State == EntityState.Added)
                {
                    skill.CreatedAt = DateTime.UtcNow;
                    skill.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    skill.UpdatedAt = DateTime.UtcNow;
                }
            }
            else if (entry.Entity is EducationEntity education)
            {
                if (entry.State == EntityState.Added)
                {
                    education.CreatedAt = DateTime.UtcNow;
                    education.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    education.UpdatedAt = DateTime.UtcNow;
                }
            }
            else if (entry.Entity is ExperienceEntity experience)
            {
                if (entry.State == EntityState.Added)
                {
                    experience.CreatedAt = DateTime.UtcNow;
                    experience.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    experience.UpdatedAt = DateTime.UtcNow;
                }
            }
            else if (entry.Entity is ContactEntity contact)
            {
                if (entry.State == EntityState.Added)
                {
                    contact.CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
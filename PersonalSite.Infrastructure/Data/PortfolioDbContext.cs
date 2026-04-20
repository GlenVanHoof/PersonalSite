using Microsoft.EntityFrameworkCore;
using PersonalSite.Infrastructure.Models;
using System.Reflection;

namespace PersonalSite.Infrastructure.Data;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
        : base(options)
    {
    }

    // DbSets
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<SkillEntity> Skills { get; set; }
    public DbSet<ExperienceEntity> Experiences { get; set; }
    public DbSet<EducationEntity> Educations { get; set; }
    public DbSet<CertificateEntity> Certificates { get; set; }
    public DbSet<ContactFormEntity> ContactForms { get; set; }
    public DbSet<PictureEntity> Pictures { get; set; }
    public DbSet<GalleryPictureEntity> GalleryPictures { get; set; }
    public DbSet<ContentItemEntity> ContentItems { get; set; }
    public DbSet<ContentFieldEntity> ContentFields { get; set; }
    public DbSet<ContentTranslationEntity> ContentTranslations { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configure UTC conversion for all DateTime properties
        ConfigureUtcConversion(modelBuilder);
    }

    private static void ConfigureUtcConversion(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(
                        new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }
        }
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

        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                SetCreatedOn(entry.Entity, now);
                SetUpdatedOn(entry.Entity, now);
            }
            else if (entry.State == EntityState.Modified)
            {
                SetUpdatedOn(entry.Entity, now);
            }
        }
    }

    private static void SetCreatedOn(object entity, DateTime now)
    {
        switch (entity)
        {
            case ProjectEntity e:
                e.CreatedOn = now;
                break;
            case SkillEntity e:
                e.CreatedOn = now;
                break;
            case ExperienceEntity e:
                e.CreatedOn = now;
                break;
            case EducationEntity e:
                e.CreatedOn = now;
                break;
            case CertificateEntity e:
                e.CreatedOn = now;
                break;
            case ContactFormEntity e:
                e.CreatedOn = now;
                break;
            case PictureEntity e:
                e.CreatedOn = now;
                break;
            case ContentItemEntity e:
                e.CreatedOn = now;
                break;
            case ContentTranslationEntity e:
                e.CreatedOn = now;
                break;
            case UserEntity e:
                e.CreatedOn = now;
                break;
        }
    }

    private static void SetUpdatedOn(object entity, DateTime now)
    {
        switch (entity)
        {
            case ProjectEntity e:
                e.UpdatedOn = now;
                break;
            case SkillEntity e:
                e.UpdatedOn = now;
                break;
            case ExperienceEntity e:
                e.UpdatedOn = now;
                break;
            case EducationEntity e:
                e.UpdatedOn = now;
                break;
            case CertificateEntity e:
                e.UpdatedOn = now;
                break;
            case PictureEntity e:
                e.UpdatedOn = now;
                break;
            case ContentItemEntity e:
                e.UpdatedOn = now;
                break;
            case ContentTranslationEntity e:
                e.UpdatedOn = now;
                break;
            case UserEntity e:
                e.UpdatedOn = now;
                break;
        }
    }
}
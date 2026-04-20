using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(e => e.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(e => e.FirstName)
            .HasMaxLength(100);
        
        builder.Property(e => e.LastName)
            .HasMaxLength(100);
        
        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
        
        builder.Property(e => e.Role)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValue("Admin");

        builder.Property(e => e.CreatedOn)
            .IsRequired();

        builder.Property(e => e.UpdatedOn)
            .IsRequired();
        
        // Unique constraints
        builder.HasIndex(e => e.Username)
            .IsUnique();
        
        builder.HasIndex(e => e.Email)
            .IsUnique();
        
        builder.HasIndex(e => e.IsActive);

        // Seed default admin user (wachtwoord: Admin123!)
        // Je moet dit later wijzigen!
        builder.HasData(
            new UserEntity
            {
                Id = 1,
                Username = "admin",
                Email = "admin@personalsite.local",
                // Dit is het gehashte wachtwoord voor "Admin123!" met BCrypt
                // Je moet dit na eerste login wijzigen!
                PasswordHash = "$2a$11$Zqz9J5W5Z5Z5Z5Z5Z5Z5ZeJ5Z5Z5Z5Z5Z5Z5Z5Z5Z5Z5Z5Z5Z5Z5Z", // Placeholder - zie hieronder
                FirstName = "Admin",
                LastName = "User",
                IsActive = true,
                Role = "SuperAdmin",
                CreatedOn = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedOn = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
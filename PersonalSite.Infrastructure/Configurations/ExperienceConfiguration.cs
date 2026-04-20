using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class ExperienceConfiguration : IEntityTypeConfiguration<ExperienceEntity>
{
    public void Configure(EntityTypeBuilder<ExperienceEntity> builder)
    {
        builder.ToTable("Experiences");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.CreatedOn)
            .IsRequired();

        builder.Property(e => e.UpdatedOn)
            .IsRequired();
        
        builder.HasIndex(e => e.StartDate);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class EducationConfiguration : IEntityTypeConfiguration<EducationEntity>
{
    public void Configure(EntityTypeBuilder<EducationEntity> builder)
    {
        builder.ToTable("Educations");
        
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
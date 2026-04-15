using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class PictureConfiguration : IEntityTypeConfiguration<PictureEntity>
{
    public void Configure(EntityTypeBuilder<PictureEntity> builder)
    {
        builder.ToTable("Pictures");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Source)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.CreatedOn)
            .IsRequired();

        builder.Property(e => e.UpdatedOn)
            .IsRequired();
        
        // Relationship with Project
        builder.HasOne(e => e.Project)
            .WithMany(p => p.Pictures)
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
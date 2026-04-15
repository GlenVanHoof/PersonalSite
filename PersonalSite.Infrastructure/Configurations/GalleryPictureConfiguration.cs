using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Configurations;

public class GalleryPictureConfiguration : IEntityTypeConfiguration<GalleryPictureEntity>
{
    public void Configure(EntityTypeBuilder<GalleryPictureEntity> builder)
    {
        builder.ToTable("GalleryPictures");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Position)
            .IsRequired();
        
        // Relationship with Picture
        builder.HasOne(e => e.Picture)
            .WithMany(p => p.GalleryPictures)
            .HasForeignKey(e => e.PictureId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
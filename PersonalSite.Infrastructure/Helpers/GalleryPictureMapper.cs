using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class GalleryPictureMapper
{
    public static GalleryPicture ToDomain(GalleryPictureEntity entity)
    {
        return new GalleryPicture
        {
            Id = entity.Id,
            Position = entity.Position,
            PictureId = entity.PictureId,
            Picture = entity.Picture == null ? null : new Picture
            {
                Id = entity.Picture.Id,
                Source = entity.Picture.Source,
                ProjectId = entity.Picture.ProjectId,
                CreatedOn = entity.Picture.CreatedOn,
                UpdatedOn = entity.Picture.UpdatedOn
            }
        };
    }

    public static GalleryPictureEntity ToEntity(GalleryPicture domain)
    {
        return new GalleryPictureEntity
        {
            Id = domain.Id,
            Position = domain.Position,
            PictureId = domain.PictureId
        };
    }
}
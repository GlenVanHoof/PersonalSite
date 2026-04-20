using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class PictureMapper
{
    public static Picture ToDomain(PictureEntity entity)
    {
        return new Picture
        {
            Id = entity.Id,
            Source = entity.Source,
            ProjectId = entity.ProjectId,
            CreatedOn = entity.CreatedOn,
            UpdatedOn = entity.UpdatedOn
        };
    }

    public static PictureEntity ToEntity(Picture domain)
    {
        return new PictureEntity
        {
            Id = domain.Id,
            Source = domain.Source,
            ProjectId = domain.ProjectId,
            CreatedOn = domain.CreatedOn,
            UpdatedOn = domain.UpdatedOn
        };
    }
}
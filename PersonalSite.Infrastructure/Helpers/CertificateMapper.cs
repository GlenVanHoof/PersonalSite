using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class CertificateMapper
{
    public static Certificate ToModel(CertificateEntity entity)
    {
        if (entity == null) return null;

        return new Certificate
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            AcquiredOn = entity.AcquiredOn,
            Organisation = entity.Organisation,
            CreatedAt = default,
            UpdatedAt = default
        };
    }

    public static CertificateEntity ToEntity(Certificate model)
    {
        if (model == null) return null;

        return new CertificateEntity
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            AcquiredOn = model.AcquiredOn,
            Organisation = model.Organisation
        };
    }

    public static IEnumerable<Certificate> ToModelList(IEnumerable<CertificateEntity> entities)
    {
        return entities?.Select(ToModel) ?? Enumerable.Empty<Certificate>();
    }

    public static IEnumerable<CertificateEntity> ToEntityList(IEnumerable<Certificate> models)
    {
        return models?.Select(ToEntity) ?? Enumerable.Empty<CertificateEntity>();
    }
}
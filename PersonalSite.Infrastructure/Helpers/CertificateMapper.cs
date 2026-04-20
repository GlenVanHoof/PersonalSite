using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Helpers;

public static class CertificateMapper
{
    public static async Task<Certificate> ToDomainAsync(CertificateEntity entity, TranslationHelper translationHelper)
    {
        var translations = await translationHelper.GetAllTranslationsAsync("Certificate", entity.Id);

        return new Certificate
        {
            Id = entity.Id,
            AcquiredOn = entity.AcquiredOn,
            Organisation = entity.Organisation,
            CreatedOn = entity.CreatedOn,
            UpdatedOn = entity.UpdatedOn,
            Name = translations.GetValueOrDefault("Name") ?? new Dictionary<string, string>(),
            Description = translations.GetValueOrDefault("Description") ?? new Dictionary<string, string>()
        };
    }

    public static CertificateEntity ToEntity(Certificate domain)
    {
        return new CertificateEntity
        {
            Id = domain.Id,
            AcquiredOn = domain.AcquiredOn,
            Organisation = domain.Organisation,
            CreatedOn = domain.CreatedOn,
            UpdatedOn = domain.UpdatedOn
        };
    }

    public static Dictionary<string, Dictionary<string, string>> ExtractTranslations(Certificate domain)
    {
        return new Dictionary<string, Dictionary<string, string>>
        {
            ["Name"] = domain.Name,
            ["Description"] = domain.Description
        };
    }
}
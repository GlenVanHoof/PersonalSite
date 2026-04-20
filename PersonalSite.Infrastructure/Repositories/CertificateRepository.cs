using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces.Repositories;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class CertificateRepository : ICertificateRepository
{
    private readonly PortfolioDbContext _context;
    private readonly TranslationHelper _translationHelper;

    public CertificateRepository(PortfolioDbContext context, TranslationHelper translationHelper)
    {
        _context = context;
        _translationHelper = translationHelper;
    }

    public async Task<IEnumerable<Certificate>> GetAllCertificatesAsync()
    {
        var entities = await _context.Certificates.OrderByDescending(c => c.AcquiredOn).ToListAsync();
        var certificates = new List<Certificate>();

        foreach (var entity in entities)
        {
            certificates.Add(await CertificateMapper.ToDomainAsync(entity, _translationHelper));
        }

        return certificates;
    }

    public async Task<Certificate?> GetCertificateByIdAsync(int id)
    {
        var entity = await _context.Certificates.FindAsync(id);
        return entity == null ? null : await CertificateMapper.ToDomainAsync(entity, _translationHelper);
    }

    public async Task<Certificate> CreateCertificateAsync(Certificate certificate)
    {
        var entity = CertificateMapper.ToEntity(certificate);
        _context.Certificates.Add(entity);
        await _context.SaveChangesAsync();

        var translations = CertificateMapper.ExtractTranslations(certificate);
        await _translationHelper.SaveAllTranslationsAsync("Certificate", entity.Id, translations);

        return await GetCertificateByIdAsync(entity.Id) ?? certificate;
    }

    public async Task UpdateCertificateAsync(Certificate certificate)
    {
        var entity = await _context.Certificates.FindAsync(certificate.Id);
        if (entity == null)
            throw new KeyNotFoundException($"Certificate with ID {certificate.Id} not found.");

        entity.AcquiredOn = certificate.AcquiredOn;
        entity.Organisation = certificate.Organisation;

        _context.Certificates.Update(entity);
        await _context.SaveChangesAsync();

        var translations = CertificateMapper.ExtractTranslations(certificate);
        await _translationHelper.SaveAllTranslationsAsync("Certificate", entity.Id, translations);
    }

    public async Task DeleteCertificateAsync(int id)
    {
        var entity = await _context.Certificates.FindAsync(id);
        if (entity != null)
        {
            await _translationHelper.DeleteTranslationsAsync("Certificate", id);
            _context.Certificates.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
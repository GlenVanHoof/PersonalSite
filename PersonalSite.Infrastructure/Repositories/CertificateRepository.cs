using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class CertificateRepository : ICertificateRepository
{
    private readonly PortfolioDbContext _context;

    public CertificateRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Certificate>> GetAllCertificatesAsync()
    {
        var entities = await _context.Certificates
            .OrderByDescending(c => c.AcquiredOn)
            .ToListAsync();

        return CertificateMapper.ToModelList(entities);
    }

    public async Task<Certificate?> GetCertificateByIdAsync(int id)
    {
        var entity = await _context.Certificates.FindAsync(id);
        return entity != null ? CertificateMapper.ToModel(entity) : null;
    }

    public async Task<Certificate> CreateCertificateAsync(Certificate certificate)
    {
        var entity = CertificateMapper.ToEntity(certificate);
        _context.Certificates.Add(entity);
        await _context.SaveChangesAsync();
        return CertificateMapper.ToModel(entity);
    }

    public async Task UpdateCertificateAsync(Certificate certificate)
    {
        var entity = CertificateMapper.ToEntity(certificate);
        _context.Certificates.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCertificateAsync(int id)
    {
        var certificate = await _context.Certificates.FindAsync(id);
        if (certificate != null)
        {
            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();
        }
    }
}
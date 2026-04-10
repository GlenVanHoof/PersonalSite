using Microsoft.EntityFrameworkCore;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Interfaces;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Repositories;

public class CertificateRepository : ICertificateRepository
{
    private readonly PortfolioDbContext _context;

    public CertificateRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CertificateEntity>> GetAllCertificatesAsync()
    {
        return await _context.Certificates
            .OrderByDescending(c => c.AcquiredOn)
            .ToListAsync();
    }

    public async Task<CertificateEntity?> GetCertificateByIdAsync(int id)
    {
        return await _context.Certificates.FindAsync(id);
    }

    public async Task<CertificateEntity> CreateCertificateAsync(CertificateEntity certificate)
    {
        _context.Certificates.Add(certificate);
        await _context.SaveChangesAsync();
        return certificate;
    }

    public async Task UpdateCertificateAsync(CertificateEntity certificate)
    {
        _context.Certificates.Update(certificate);
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
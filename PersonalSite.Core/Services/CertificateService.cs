using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;

namespace PersonalSite.Core.Services;

public class CertificateService : ICertificateService
{
    private readonly ICertificateRepository _certificateRepository;

    public CertificateService(ICertificateRepository certificateRepository)
    {
        _certificateRepository = certificateRepository;
    }

    public async Task<IEnumerable<Certificate>> GetAllCertificatesAsync()
    {
        return await _certificateRepository.GetAllCertificatesAsync();
    }

    public async Task<Certificate?> GetCertificateByIdAsync(int id)
    {
        return await _certificateRepository.GetCertificateByIdAsync(id);
    }

    public async Task<Certificate> CreateCertificateAsync(Certificate certificate)
    {
        return await _certificateRepository.CreateCertificateAsync(certificate);
    }

    public async Task UpdateCertificateAsync(Certificate certificate)
    {
        await _certificateRepository.UpdateCertificateAsync(certificate);
    }

    public async Task DeleteCertificateAsync(int id)
    {
        await _certificateRepository.DeleteCertificateAsync(id);
    }
}
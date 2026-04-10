using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Interfaces;

public interface ICertificateRepository
{
    Task<IEnumerable<CertificateEntity>> GetAllCertificatesAsync();
    Task<CertificateEntity?> GetCertificateByIdAsync(int id);
    Task<CertificateEntity> CreateCertificateAsync(CertificateEntity certificate);
    Task UpdateCertificateAsync(CertificateEntity certificate);
    Task DeleteCertificateAsync(int id);
}
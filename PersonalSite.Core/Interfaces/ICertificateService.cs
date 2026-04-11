using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces;

public interface ICertificateService
{
    Task<IEnumerable<Certificate>> GetAllCertificatesAsync();
    Task<Certificate?> GetCertificateByIdAsync(int id);
    Task<Certificate> CreateCertificateAsync(Certificate certificate);
    Task UpdateCertificateAsync(Certificate certificate);
    Task DeleteCertificateAsync(int id);
}
using PersonalSite.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalSite.Core.Interfaces;

public interface ICertificateRepository
{
    Task<IEnumerable<Certificate>> GetAllCertificatesAsync();
    Task<Certificate?> GetCertificateByIdAsync(int id);
    Task<Certificate> CreateCertificateAsync(Certificate certificate);
    Task UpdateCertificateAsync(Certificate certificate);
    Task DeleteCertificateAsync(int id);
}
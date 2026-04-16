using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces.Repositories;

public interface IContactFormRepository
{
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact?> GetByIdAsync(int id);
    Task<Contact> CreateAsync(Contact contact);
    Task DeleteAsync(int id);
}
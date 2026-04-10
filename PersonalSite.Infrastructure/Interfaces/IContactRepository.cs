using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Interfaces;

public interface IContactRepository
{
    Task<IEnumerable<ContactEntity>> GetAllContactsAsync();
    Task<ContactEntity?> GetContactByIdAsync(int id);
    Task<ContactEntity> CreateContactAsync(ContactEntity contact);
    Task UpdateContactAsync(ContactEntity contact);
    Task DeleteContactAsync(int id);
}
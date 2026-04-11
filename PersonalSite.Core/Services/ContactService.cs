using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;

namespace PersonalSite.Core.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        return await _contactRepository.GetAllContactsAsync();
    }

    public async Task<Contact?> GetContactByIdAsync(int id)
    {
        return await _contactRepository.GetContactByIdAsync(id);
    }

    public async Task<Contact> CreateContactAsync(Contact contact)
    {
        return await _contactRepository.CreateContactAsync(contact);
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        await _contactRepository.UpdateContactAsync(contact);
    }

    public async Task DeleteContactAsync(int id)
    {
        await _contactRepository.DeleteContactAsync(id);
    }
}
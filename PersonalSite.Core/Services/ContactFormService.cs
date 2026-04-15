using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;

namespace PersonalSite.Core.Services;

public class ContactFormService : IContactFormService
{
    private readonly IContactFormRepository _contactFormRepository;

    public ContactFormService(IContactFormRepository contactFormRepository)
    {
        _contactFormRepository = contactFormRepository;
    }

    public async Task<IEnumerable<Contact>> GetAllContactFormsAsync()
    {
        return await _contactFormRepository.GetAllAsync();
    }

    public async Task<Contact?> GetContactFormByIdAsync(int id)
    {
        return await _contactFormRepository.GetByIdAsync(id);
    }

    public async Task<Contact> SubmitContactFormAsync(Contact contact)
    {
        // Business logic: ensure CreatedOn is set
        contact.CreatedOn = DateTime.UtcNow;
        return await _contactFormRepository.CreateAsync(contact);
    }

    public async Task DeleteContactFormAsync(int id)
    {
        await _contactFormRepository.DeleteAsync(id);
    }
}
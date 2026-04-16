using PersonalSite.Core.Models;

namespace PersonalSite.Core.Interfaces.Services;

public interface IContactFormService
{
    Task<IEnumerable<Contact>> GetAllContactFormsAsync();
    Task<Contact?> GetContactFormByIdAsync(int id);
    Task<Contact> SubmitContactFormAsync(Contact contact);
    Task DeleteContactFormAsync(int id);
}
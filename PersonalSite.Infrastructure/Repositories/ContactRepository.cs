using Microsoft.EntityFrameworkCore;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Interfaces;
using PersonalSite.Infrastructure.Models;

namespace PersonalSite.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly PortfolioDbContext _context;

    public ContactRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContactEntity>> GetAllContactsAsync()
    {
        return await _context.Contacts
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<ContactEntity?> GetContactByIdAsync(int id)
    {
        return await _context.Contacts.FindAsync(id);
    }

    public async Task<ContactEntity> CreateContactAsync(ContactEntity contact)
    {
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    public async Task UpdateContactAsync(ContactEntity contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContactAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}
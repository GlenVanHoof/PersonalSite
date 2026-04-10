using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Models;
using PersonalSite.Core.Interfaces;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly PortfolioDbContext _context;

    public ContactRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        var entities = await _context.Contacts
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
        
        return ContactMapper.ToModelList(entities);
    }

    public async Task<Contact?> GetContactByIdAsync(int id)
    {
        var entity = await _context.Contacts.FindAsync(id);
        return entity != null ? ContactMapper.ToModel(entity) : null;
    }

    public async Task<Contact> CreateContactAsync(Contact contact)
    {
        var entity = ContactMapper.ToEntity(contact);
        _context.Contacts.Add(entity);
        await _context.SaveChangesAsync();
        return ContactMapper.ToModel(entity);
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        var entity = ContactMapper.ToEntity(contact);
        _context.Contacts.Update(entity);
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
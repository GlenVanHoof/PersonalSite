using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Helpers;

namespace PersonalSite.Infrastructure.Repositories;

public class ContactFormRepository : IContactFormRepository
{
    private readonly PortfolioDbContext _context;

    public ContactFormRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        var entities = await _context.ContactForms
            .OrderByDescending(c => c.CreatedOn)
            .ToListAsync();

        return entities.Select(ContactFormMapper.ToDomain);
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        var entity = await _context.ContactForms.FindAsync(id);
        return entity == null ? null : ContactFormMapper.ToDomain(entity);
    }

    public async Task<Contact> CreateAsync(Contact contact)
    {
        var entity = ContactFormMapper.ToEntity(contact);
        _context.ContactForms.Add(entity);
        await _context.SaveChangesAsync();
        return ContactFormMapper.ToDomain(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.ContactForms.FindAsync(id);
        if (entity != null)
        {
            _context.ContactForms.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
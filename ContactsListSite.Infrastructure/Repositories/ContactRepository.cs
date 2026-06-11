using ContactsListSite.Domain.Models;
using ContactsListSite.Domain.RepInterfaces;
using ContactsListSite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsListSite.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
           _context = context;
        }

        public async Task<List<Contact>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Contacts.ToListAsync(ct);
        }

        public async Task<Contact?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Contacts.FindAsync(id, ct);
        }

        public void Add(Contact contact)
        {
            _context.Add(contact);
        }

        public async Task<bool> Delete(Guid id, CancellationToken ct)
        {
            var affectedRows = await _context.Contacts.Where(c => c.Id == id).ExecuteDeleteAsync(ct);

            return affectedRows > 0;
        }

        public async Task<bool> Update(Contact contact, CancellationToken ct)
        {
            var affectedRows = await _context.Contacts
            .Where(c => c.Id == contact.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.FirstName, contact.FirstName)
                .SetProperty(x => x.LastName, contact.LastName)
                .SetProperty(x => x.MobilePhone, contact.MobilePhone)
                .SetProperty(x => x.JobTitle, contact.JobTitle)
                .SetProperty(x => x.BirthDate, contact.BirthDate),
                ct);

            return affectedRows > 0;
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
                await _context.SaveChangesAsync(ct);
        }
    }
}

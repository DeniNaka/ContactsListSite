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
                return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id, ct);
            }

            public void Add(Contact contact)
            {
                _context.Add(contact);
            }

            public void Delete(Contact contact)
            {
                _context.Contacts.Remove(contact);
            }

            public async Task SaveChangesAsync(CancellationToken ct)
            {
                await _context.SaveChangesAsync(ct);
            }
        }
    }

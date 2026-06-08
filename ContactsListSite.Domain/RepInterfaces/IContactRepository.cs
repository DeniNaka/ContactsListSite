using ContactsListSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsListSite.Domain.RepInterfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync(CancellationToken ct);
        Task<Contact?> GetByIdAsync(Guid id, CancellationToken ct);
        void Add(Contact contact);
        void Delete(Contact contact);
        Task SaveChangesAsync(CancellationToken ct);
    }
}

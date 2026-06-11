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
        Task<bool> Delete(Guid id, CancellationToken ct);
        Task<bool> Update(Contact contact, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}

using ContactsListSite.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsListSite.Application.ServInterfaces
{
    public interface IContactService
    {
        Task<List<ContactDto>> GetAllContactsAsync(CancellationToken ct);
        Task<ContactDto> GetContactByIdAsync(Guid id, CancellationToken ct);
        Task<ContactDto> CreateContactAsync(CreateContactDto dto, CancellationToken ct);
        Task UpdateContactAsync(Guid id, UpdateContactDto dto, CancellationToken ct);
        Task DeleteContactAsync(Guid id, CancellationToken ct);
    }
}

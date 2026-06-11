using ContactsListSite.Application.DTOs;
using ContactsListSite.Application.Mappings;
using ContactsListSite.Application.ServInterfaces;
using ContactsListSite.Domain.DomainExceptions;
using ContactsListSite.Domain.Models;
using ContactsListSite.Domain.RepInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsListSite.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<List<ContactDto>> GetAllContactsAsync(CancellationToken ct)
        {
            var contacts = await _contactRepository.GetAllAsync(ct);

            return contacts.Select(c => c.ToDto()).ToList();
        }

        public async Task<ContactDto> GetContactByIdAsync(Guid id, CancellationToken ct)
        {
            var contact = await _contactRepository.GetByIdAsync(id, ct);

            if (contact == null)
            {
                throw new NotFoundException($"Contact with id '{id}' not found.");
            }

            return contact.ToDto();
        }

        public async Task<ContactDto> CreateContactAsync(CreateContactDto dto, CancellationToken ct)
        {
            var contact = dto.ToEntity();

            _contactRepository.Add(contact);
            await _contactRepository.SaveChangesAsync(ct);

            return contact.ToDto();
        }

        public async Task UpdateContactAsync(Guid id, UpdateContactDto dto, CancellationToken ct)
        {
            var updated = await _contactRepository.Update(dto.ToEntity(id), ct);

            if (!updated)
            {
                throw new NotFoundException($"Contact with id '{id}' not found.");
            }
        }

        public async Task DeleteContactAsync(Guid id, CancellationToken ct)
        {
            var deleted = await _contactRepository.Delete(id, ct);

            if (!deleted)
            {
                throw new NotFoundException($"Contact with id '{id}' not found.");
            }
        }
    }
}

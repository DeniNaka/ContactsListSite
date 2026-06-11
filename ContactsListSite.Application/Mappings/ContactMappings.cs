using ContactsListSite.Application.DTOs;
using ContactsListSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsListSite.Application.Mappings
{
    public static class ContactMappings
    {
        public static ContactDto ToDto(this Contact contact)
        {
            return new ContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                MobilePhone = contact.MobilePhone,
                JobTitle = contact.JobTitle,
                BirthDate = contact.BirthDate
            };
        }

        public static Contact ToEntity(this CreateContactDto dto)
        {
            return new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MobilePhone = dto.MobilePhone,
                JobTitle = dto.JobTitle,
                BirthDate = dto.BirthDate
            };
        }

        public static Contact ToEntity(this UpdateContactDto dto, Guid id)
        {
            return new Contact
            {
                Id = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MobilePhone = dto.MobilePhone,
                JobTitle = dto.JobTitle,
                BirthDate = dto.BirthDate
            };
        }
    }
}

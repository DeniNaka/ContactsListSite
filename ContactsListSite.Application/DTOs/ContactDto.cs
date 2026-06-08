using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsListSite.Application.DTOs
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string MobilePhone { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public DateTime BirthDate { get; set; }
    }
}

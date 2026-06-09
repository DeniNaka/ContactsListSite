using ContactsListSite.Application.DTOs;
using ContactsListSite.Application.ServInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsListSite.Api.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var contacts = await _service.GetAllContactsAsync(ct);
            return Ok(contacts);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var contact = await _service.GetContactByIdAsync(id, ct);
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactDto dto, CancellationToken ct)
        {
            var contact = await _service.CreateContactAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
        } 

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContactDto dto, CancellationToken ct)
        {
            await _service.UpdateContactAsync(id, dto, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _service.DeleteContactAsync(id, ct);
            return NoContent();
        }
    }
}

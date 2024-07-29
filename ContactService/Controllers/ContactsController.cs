using ContactService.DTO;
using ContactService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactServices _contactService;

        public ContactsController(IContactServices contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> Get()
        {
            var contacts = await _contactService.GetAllAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDTO>> Get(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ContactDTO contactDto)
        {
            await _contactService.CreateAsync(contactDto);
            return CreatedAtAction(nameof(Get), new { id = contactDto.Id }, contactDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ContactDTO contactDto)
        {
            if (id != contactDto.Id) return BadRequest();
            await _contactService.UpdateAsync(contactDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _contactService.DeleteAsync(id);
            return NoContent();
        }
    }
}

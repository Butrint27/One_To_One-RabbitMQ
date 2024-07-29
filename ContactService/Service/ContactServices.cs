using AutoMapper;
using ContactService.Data;
using ContactService.DTO;
using ContactService.Model;
using MassTransit;
using Messaging.Shared.Events;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Service
{
    public class ContactServices : IContactServices
    {
        private readonly ContactDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly HttpClient _httpClient;

        public ContactServices(ContactDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint, HttpClient httpClient)
        {
            _context = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ContactDTO>> GetAllAsync()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return _mapper.Map<IEnumerable<ContactDTO>>(contacts);
        }

        public async Task<ContactDTO> GetByIdAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            return contact != null ? _mapper.Map<ContactDTO>(contact) : null;
        }

        public async Task CreateAsync(ContactDTO contactDto)
        {
            if (!await VerifyStudentExists(contactDto.StudentId))
                throw new Exception("Student does not exist.");

            var contact = _mapper.Map<Contact>(contactDto);
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            await _publishEndpoint.Publish(new ContactCreated
            {
                ContactId = contact.Id,
                StudentId = contact.StudentId,
                Email = contact.Email
            });
        }

        public async Task UpdateAsync(ContactDTO contactDto)
        {
            var contact = await _context.Contacts.FindAsync(contactDto.Id);
            if (contact == null) throw new Exception("Contact not found");

            _mapper.Map(contactDto, contact);
            await _context.SaveChangesAsync();

            await _publishEndpoint.Publish(new ContactUpdated
            {
                ContactId = contact.Id,
                StudentId = contact.StudentId,
                Email = contact.Email
            });
        }

        public async Task DeleteAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) throw new Exception("Contact not found");

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            await _publishEndpoint.Publish(new ContactDeleted
            {
                ContactId = id
            });
        }

        private async Task<bool> VerifyStudentExists(int studentId)
        {
            var response = await _httpClient.GetAsync($"/api/students/{studentId}");
            return response.IsSuccessStatusCode;
        }
    }
}

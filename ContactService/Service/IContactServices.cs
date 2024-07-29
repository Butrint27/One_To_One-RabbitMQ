using ContactService.DTO;

namespace ContactService.Service
{
    public interface IContactServices
    {
        Task<IEnumerable<ContactDTO>> GetAllAsync();
        Task<ContactDTO> GetByIdAsync(int id);
        Task CreateAsync(ContactDTO contact);
        Task UpdateAsync(ContactDTO contact);
        Task DeleteAsync(int id);
    }
}

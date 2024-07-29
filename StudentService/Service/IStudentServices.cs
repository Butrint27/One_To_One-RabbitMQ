using StudentService.DTO;

namespace StudentService.Service
{
    public interface IStudentServices
    {
        Task<IEnumerable<StudentDTO>> GetAllAsync();
        Task<StudentDTO> GetByIdAsync(int id);
        Task CreateAsync(StudentDTO student);
        Task UpdateAsync(StudentDTO student);
        Task DeleteAsync(int id);
    }
}

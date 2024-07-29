using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.DTO;
using StudentService.Model;
using Messaging.Shared.Events;

namespace StudentService.Service
{
    public class StudentServices : IStudentServices
    {
        private readonly StudentDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public StudentServices(StudentDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            var students = await _context.Students.ToListAsync();
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<StudentDTO> GetByIdAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            return _mapper.Map<StudentDTO>(student);
        }

        public async Task CreateAsync(StudentDTO studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            await _publishEndpoint.Publish(new StudentCreated { StudentId = student.Id, Name = student.Name });
        }

        public async Task UpdateAsync(StudentDTO studentDto)
        {
            var student = await _context.Students.FindAsync(studentDto.Id);
            if (student == null) return;

            _mapper.Map(studentDto, student);
            await _context.SaveChangesAsync();
            await _publishEndpoint.Publish(new StudentUpdated { StudentId = student.Id, Name = student.Name });
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            await _publishEndpoint.Publish(new StudentDeleted { StudentId = id });
        }
    }
}

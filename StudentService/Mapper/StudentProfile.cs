using AutoMapper;
using StudentService.DTO;
using StudentService.Model;

namespace StudentService.Mapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
        }
    }
}

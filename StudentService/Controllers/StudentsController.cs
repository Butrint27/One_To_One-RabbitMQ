using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentService.DTO;
using StudentService.Service;

namespace StudentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentServices _studentService;

        public StudentsController(IStudentServices studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> Get()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> Get(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult> Create(StudentDTO studentDto)
        {
            await _studentService.CreateAsync(studentDto);
            return CreatedAtAction(nameof(Get), new { id = studentDto.Id }, studentDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, StudentDTO studentDto)
        {
            if (id != studentDto.Id) return BadRequest();
            await _studentService.UpdateAsync(studentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }
}

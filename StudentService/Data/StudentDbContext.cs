using Microsoft.EntityFrameworkCore;
using StudentService.Model;

namespace StudentService.Data
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }
    }
}

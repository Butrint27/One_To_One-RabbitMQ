using ContactService.Model;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Data
{
    public class ContactDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ContactDbContext(DbContextOptions<ContactDbContext> options)
            : base(options) { }
    }
}

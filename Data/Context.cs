using Microsoft.EntityFrameworkCore;
using curso_api.Model;

namespace curso_api.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {}

        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
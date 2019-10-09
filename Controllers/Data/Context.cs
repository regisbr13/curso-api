using Microsoft.EntityFrameworkCore;
using MinhaApi.Controllers.Model;

namespace MinhaApi.Controllers.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {}

        public DbSet<Person> Persons { get; set; }
    }
}
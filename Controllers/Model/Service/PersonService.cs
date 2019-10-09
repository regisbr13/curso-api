using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Controllers.Data;
using MinhaApi.Controllers.Model.Interfaces.Service;

namespace MinhaApi.Controllers.Model.Service
{
    public class PersonService : IPersonService
    {
        private readonly Context _context;

        public PersonService(Context context) {
            _context = context;
        }
        public async Task<List<Person>> FindAll()
        {
            var persons = await _context.Persons.ToListAsync();
            return persons;
        }

        public async Task<Person> FindById(long id)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);
            return person;
        }

        public async Task<Person> Insert(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            return await FindById(person.Id);
        }

        public async Task Remove(long id)
        {
            var person = await FindById(id);
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> Update(Person person)
        {
            var personUpdated = await FindById(person.Id);
                _context.Entry(personUpdated).CurrentValues.SetValues(person);
                await _context.SaveChangesAsync();
                return person;
        }
    }
}
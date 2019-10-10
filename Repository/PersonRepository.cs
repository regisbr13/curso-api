using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using curso_api.Data;
using curso_api.Model;
using curso_api.Repository.Interfaces;

// CAMADA DE PERSISTÊNCIA
namespace curso_api.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly Context _context;

        public PersonRepository(Context context) {
            _context = context;
        }
        
        public async Task<List<Person>> FindAllAsync()
        {
            var persons = await _context.Persons.ToListAsync();
            return persons;
        }

        public async Task<Person> FindByIdAsync(long id)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);
            return person;
        }

        public async Task<Person> InsertAsync(Person person)
        {
            try {
                await _context.Persons.AddAsync(person);
                await _context.SaveChangesAsync();
                return await FindByIdAsync(person.Id);
            } 
            catch(Exception ex) {
                throw ex;
            }
        }

        public async Task RemoveAsync(long id)
        {
            try {
                var person = await FindByIdAsync(id);
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            } 
            catch(Exception ex) {
                throw ex;
            }

        }

        public async Task<Person> UpdateAsync(Person person)
        {
            try {
                var updatedPerson = await FindByIdAsync(person.Id);
                if(updatedPerson == null) return null;
                _context.Entry(updatedPerson).CurrentValues.SetValues(person);
                await _context.SaveChangesAsync();
                return person;
            } 
            catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> ExistsAsync(Person person) {
            return await _context.Persons.AnyAsync(x => x.Id == person.Id);
        }
    }
}